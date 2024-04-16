using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Exceptions;

namespace AirportTicketBookingSystem.Flights
{
    public class FlightManagement
    {
        public FlightManagement()
        {
            Flights = new List<Flight>();
            PassengerFlights = new List<Flight>();
            AddDefaultFlight();
        }

        public List<Flight> Flights { get; set; }
        public List<Flight> PassengerFlights { get; set; }

        public async Task BatchFlightUpload()
        {
            try
            {
                Console.WriteLine("Batch Flight Upload operation selected.");
                Console.WriteLine("Enter CSV file Path");
                string filePath = Console.ReadLine();
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new FlightManagementException("File path cannot be empty.");
                }

                var newFlights = await FileOperations.ReadFlightsFromCSVAsync(filePath);
                Flights.AddRange(newFlights);

                Console.WriteLine("Flights successfully uploaded.");
            }
            catch (FlightManagementException ex)
            {
                Console.WriteLine($"Flight management error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void BookFlight()
        {
            Console.WriteLine("Book a Flight operation selected.");
            Console.Write("Enter the number of flights you want to book: ");
            String? flightNumber = Console.ReadLine();

            var selectedFlight = Flights.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
            if (selectedFlight != null)
            {
                PassengerFlights.Add(selectedFlight);
                Console.WriteLine($"Flight {flightNumber} added to your bookings.");
            }
            else
            {
                Console.WriteLine($"Flight with number {flightNumber} not found.");
            }
        }

        public void CancelBooking()
        {
            Console.WriteLine("Cancel Booking operation selected.");
            Console.Write("Enter the number of bookings you want to cancel: ");
            String? flightNumber = Console.ReadLine();

            var selectedFlight = PassengerFlights.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
            if (selectedFlight != null)
            {
                PassengerFlights.Remove(selectedFlight);
                Console.WriteLine($"Flight {flightNumber} Canceled.");
            }
            else
            {
                Console.WriteLine($"Flight with number {flightNumber} not found.");
            }
        }

        public void EditBooking()
        {
            try
            {
                Console.WriteLine("Edit Booking operation selected.");
                Console.Write("Enter the number of bookings you want to edit: ");
                string? flightNumber = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(flightNumber))
                {
                    throw new ArgumentException("Flight number cannot be empty.");
                }

                var selectedBooking = PassengerFlights.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));

                if (selectedBooking == null)
                {
                    throw new FlightManagementException($"Booking for flight {flightNumber} not found.");
                }

                Console.Write($"Enter the new class for booking {flightNumber}: ");
                string newClassInput = Console.ReadLine();

                if (Enum.TryParse(newClassInput, true, out FlightClass newClass))
                {
                    selectedBooking.Class = newClass;
                    Console.WriteLine($"Booking for flight {flightNumber} updated to class {newClass}.");
                }
                else
                {
                    throw new ArgumentException($"Invalid class: {newClassInput}. Please enter a valid class.");
                }
            }
            catch (FlightManagementException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Exit()
        {
            Console.WriteLine("Exiting, BYEEEEEE");
            Environment.Exit(0);
        }

        public void FilterBookings()
        {
            Console.WriteLine("Enter parameter and value to filter flights (e.g., 'Price' = '500'):");
            Console.Write("Enter parameter: ");
            string parameterString = Console.ReadLine();
            Console.Write("Enter value: ");
            string value = Console.ReadLine();

            if (!Enum.TryParse(parameterString, true, out FilterParameter parameter))
            {
                Console.WriteLine("Invalid parameter.");
                return;
            }

            Func<Flight, bool> filterPredicate;

            switch (parameter)
            {
                case FilterParameter.FlightNumber:
                    filterPredicate = f => f.FlightNumber.Equals(value);
                    break;
                case FilterParameter.Price:
                    if (decimal.TryParse(value, out decimal price))
                        filterPredicate = f => f.Price == price;
                    else
                    {
                        Console.WriteLine("Invalid price value.");
                        return;
                    }
                    break;
                case FilterParameter.DepartureCountry:
                    filterPredicate = f => f.DepartureCountry.Equals(value);
                    break;
                case FilterParameter.DestinationCountry:
                    filterPredicate = f => f.DestinationCountry.Equals(value);
                    break;
                case FilterParameter.DepartureDate:
                    if (DateTime.TryParse(value, out DateTime departureDate))
                        filterPredicate = f => f.DepartureDate.Date == departureDate.Date;
                    else
                    {
                        Console.WriteLine("Invalid departure date value.");
                        return;
                    }
                    break;
                case FilterParameter.DepartureAirport:
                    filterPredicate = f => f.DepartureAirport.Equals(value);
                    break;
                case FilterParameter.ArrivalAirport:
                    filterPredicate = f => f.ArrivalAirport.Equals(value);
                    break;
                case FilterParameter.Class:
                    if (Enum.TryParse(value, true, out FlightClass flightClass))
                        filterPredicate = f => f.Class == flightClass;
                    else
                    {
                        Console.WriteLine("Invalid class value.");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid parameter.");
                    return;
            }

            List<Flight> filteredFlights = Flights.Where(filterPredicate).ToList();

            DisplayFilteredFlights(filteredFlights);
        }

        public void ListAllFlights()
        {
            try
            {
                Console.WriteLine("Listing all flights:\n");
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", "Flight", "Departure", "Destination", "Departure Date", "Departure Airport", "Arrival Airport", "Class", "Price");
                Console.WriteLine(new string('-', 120));
                if (Flights.Count == 0)
                {
                    throw new FlightManagementException("No flights found.");
                }

                DisplayFlights(Flights);

                Console.WriteLine("\nDo you want to book a flight? (Y/N)");
                string choice = Console.ReadLine();

                if (choice.ToUpper() == "Y")
                {
                    BookFlight();
                }
                else
                {
                    Console.WriteLine("Thank you for using our flight booking service.");
                }
            }
            catch (FlightManagementException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ViewPersonalBookings()
        {
            try
            {
                Console.WriteLine("View Personal Booking:\n");
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", "Flight", "Departure", "Destination", "Departure Date", "Departure Airport", "Arrival Airport", "Class", "Price");
                Console.WriteLine(new string('-', 120));

                // Check if the PassengerFlights list is empty
                if (PassengerFlights.Count == 0)
                {
                    throw new FlightManagementException("No bookings found.");
                }

                DisplayFlights(PassengerFlights);
            }
            catch (FlightManagementException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void AddDefaultFlight()
        {
            Flights.Add(new Flight("ABC123", "USA", "UK", DateTime.Now.AddDays(1), "JFK", "LHR", FlightClass.Business, 1000));
            Flights.Add(new Flight("RST", "USA", "UK", DateTime.Now.AddDays(1), "JFK", "LHR", FlightClass.Business, 2000));
        }

        private void DisplayFlights(List<Flight> flights)
        {
            flights.ForEach(flight =>
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", flight.FlightNumber, flight.DepartureCountry, flight.DestinationCountry, flight.DepartureDate.ToString("yyyy-MM-dd HH:mm"), flight.DepartureAirport, flight.ArrivalAirport, flight.Class, flight.Price);
            });
        }

        private void DisplayFilteredFlights(List<Flight> flights)
        {
            Console.WriteLine("Filtered Flights:");
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-15} {5,-15} {6,-10} {7,-10}", "Flight Number", "Departure Country", "Destination Country", "Departure Date", "Departure Airport", "Arrival Airport", "Class", "Price");
            Console.WriteLine(new string('-', 120));

            DisplayFlights(flights);
        }
        public void Search()
        {
            Console.WriteLine("Enter search criteria for flights:");
            Console.Write("Enter Flight Number: ");
            string flightNumber = Console.ReadLine();
            Console.Write("Enter Departure Country: ");
            string departureCountry = Console.ReadLine();
            Console.Write("Enter Destination Country: ");
            string destinationCountry = Console.ReadLine();
            Console.Write("Enter Departure Date (YYYY-MM-DD): ");
            string departureDateStr = Console.ReadLine();
            Console.Write("Enter Departure Airport: ");
            string departureAirport = Console.ReadLine();
            Console.Write("Enter Arrival Airport: ");
            string arrivalAirport = Console.ReadLine();
            Console.Write("Enter Class (Economy/Business/First): ");
            string classStr = Console.ReadLine();
            Console.Write("Enter Price: ");
            string priceStr = Console.ReadLine();

            DateTime? departureDate = null;
            if (!string.IsNullOrEmpty(departureDateStr) && DateTime.TryParse(departureDateStr, out DateTime parsedDate))
            {
                departureDate = parsedDate;
            }

            decimal? price = null;
            if (!string.IsNullOrEmpty(priceStr) && decimal.TryParse(priceStr, out decimal parsedPrice))
            {
                price = parsedPrice;
            }
            var searchResults = SearchFlights(flightNumber, departureCountry, destinationCountry, departureDate, departureAirport, arrivalAirport, classStr, price);

            DisplayFilteredFlights(searchResults);
        }

        // Search function to search flights based on multiple criteria
        private List<Flight> SearchFlights(string flightNumber, string departureCountry, string destinationCountry, DateTime? departureDate, string departureAirport, string arrivalAirport, string classStr, decimal? price)
        {
            var searchResults = Flights.Where(f =>
                (string.IsNullOrEmpty(flightNumber) || f.FlightNumber.Equals(flightNumber)) &&
                (string.IsNullOrEmpty(departureCountry) || f.DepartureCountry.Equals(departureCountry)) &&
                (string.IsNullOrEmpty(destinationCountry) || f.DestinationCountry.Equals(destinationCountry)) &&
                (!departureDate.HasValue || f.DepartureDate.Date == departureDate.Value.Date) &&
                (string.IsNullOrEmpty(departureAirport) || f.DepartureAirport.Equals(departureAirport)) &&
                (string.IsNullOrEmpty(arrivalAirport) || f.ArrivalAirport.Equals(arrivalAirport)) &&
                (string.IsNullOrEmpty(classStr) || Enum.TryParse(classStr, true, out FlightClass flightClass) && f.Class == flightClass) &&
                (!price.HasValue || f.Price == price.Value)
            ).ToList();

            return searchResults;
        }
    }
}
