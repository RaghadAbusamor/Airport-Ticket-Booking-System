using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Exceptions;

namespace AirportTicketBookingSystem.Flights
{ public class FlightManagement
    {
        public FlightManagement()
        {
            Flights = new List<Flight>();
            PassengerFlights = new List<Flight>();

            // Add a default flight
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

                // Simulating an error condition where the file path is empty
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
            String? FlightNumber = Console.ReadLine();

            foreach (var flight in Flights)
            {
                if (flight.FlightNumber.Equals(FlightNumber, StringComparison.OrdinalIgnoreCase))
                {
                    Flight selectedFlight = Flights.Find(f => f.FlightNumber.Equals(flight.FlightNumber));
                    PassengerFlights.Add(selectedFlight);
                    Console.WriteLine($"Flight {FlightNumber} added to your bookings.");
                }
                else
                {
                    Console.WriteLine($"Flight with number {FlightNumber} not found.");
                }
            }
        }

        public void CancelBooking()
        {
            Console.WriteLine("Cancel Booking operation selected.");
            Console.Write("Enter the number of bookings you want to cancel: ");
            String? FlightNumber = Console.ReadLine();

            foreach (var flight in Flights)
            {
                if (flight.FlightNumber.Equals(FlightNumber, StringComparison.OrdinalIgnoreCase))
                {
                    Flight selectedFlight = Flights.Find(f => f.FlightNumber.Equals(flight.FlightNumber));
                    PassengerFlights.Remove(selectedFlight);
                    Console.WriteLine($"Flight {FlightNumber} Canceled.");
                }
                else
                {
                    Console.WriteLine($"Flight with number {FlightNumber} not found.");
                }
            }
        }

        public void EditBooking()
        {
            Console.WriteLine("Edit Booking operation selected.");
            Console.Write("Enter the number of bookings you want to edit: ");
            String? flightNumber = Console.ReadLine();

            // Find the selected booking with the entered flight number
            Flight selectedBooking = PassengerFlights.Find(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
            if (selectedBooking != null)
            {
                Console.Write($"Enter the new class for booking {flightNumber}: ");
                string newClassInput = Console.ReadLine();

                // Parse the new class input to FlightClass enum
                if (Enum.TryParse(newClassInput, true, out FlightClass newClass))
                {
                    // Update the flight class
                    selectedBooking.Class = newClass;
                    Console.WriteLine($"Booking for flight {flightNumber} updated to class {newClass}.");
                }
                else
                {
                    Console.WriteLine($"Invalid class: {newClassInput}. Please enter a valid class.");
                }
            }
            else
            {
                Console.WriteLine($"Booking for flight {flightNumber} not found.");
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
            string parameter = Console.ReadLine();
            Console.Write("Enter value: ");
            string value = Console.ReadLine();

            List<Flight> filteredFlights = new List<Flight>();

            switch (parameter.ToLower())
            {
                case "flight number":
                    filteredFlights = Flights.Where(f => f.FlightNumber.Equals(value)).ToList();
                    break;

                case "price":
                    decimal price;
                    if (decimal.TryParse(value, out price))
                        filteredFlights = Flights.Where(f => f.Price.Equals(price)).ToList();
                    else
                        Console.WriteLine("Invalid price value.");
                    break;

                case "departure country":
                    filteredFlights = Flights.Where(f => f.DepartureCountry.Equals(value)).ToList();
                    break;

                case "destination country":
                    filteredFlights = Flights.Where(f => f.DestinationCountry.Equals(value)).ToList();
                    break;

                case "departure date":
                    DateTime departureDate;
                    if (DateTime.TryParse(value, out departureDate))
                        filteredFlights = Flights.Where(f => f.DepartureDate.Date.Equals(departureDate.Date)).ToList();
                    else
                        Console.WriteLine("Invalid departure date value.");
                    break;

                case "departure airport":
                    filteredFlights = Flights.Where(f => f.DepartureAirport.Equals(value)).ToList();
                    break;

                case "arrival airport":
                    filteredFlights = Flights.Where(f => f.ArrivalAirport.Equals(value)).ToList();
                    break;

                case "class":
                    FlightClass flightClass;
                    if (Enum.TryParse(value, true, out flightClass))
                        filteredFlights = Flights.Where(f => f.Class.Equals(flightClass)).ToList();
                    else
                        Console.WriteLine("Invalid class value.");
                    break;

                default:
                    Console.WriteLine("Invalid parameter.");
                    break;
            }

            Console.WriteLine("Filtered Flights:");
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-15} {5,-15} {6,-10} {7,-10}", "Flight Number", "Departure Country", "Destination Country", "Departure Date", "Departure Airport", "Arrival Airport", "Class", "Price");
            Console.WriteLine(new string('-', 120));

            foreach (var flight in filteredFlights)
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-15} {5,-15} {6,-10} {7,-10}",
                    flight.FlightNumber, flight.DepartureCountry, flight.DestinationCountry,
                    flight.DepartureDate.ToString("yyyy-MM-dd HH:mm"), flight.DepartureAirport,
                    flight.ArrivalAirport, flight.Class, flight.Price);
            }
        }

        public void ListAllFlights()
        {
            try
            {
                Console.WriteLine("Listing all flights:\n");
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", "Flight", "Departure", "Destination", "Departure Date", "Departure Airport", "Arrival Airport", "Class", "Price");
                Console.WriteLine(new string('-', 120));

                // Check if the Flights list is empty
                if (Flights.Count == 0)
                {
                    throw new FlightManagementException("No flights found.");
                }

                foreach (var flight in Flights)
                {
                    Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", flight.FlightNumber, flight.DepartureCountry, flight.DestinationCountry, flight.DepartureDate.ToString("yyyy-MM-dd HH:mm"), flight.DepartureAirport, flight.ArrivalAirport, flight.Class, flight.Price);
                }

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

                foreach (var flight in PassengerFlights)
                {
                    Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", flight.FlightNumber, flight.DepartureCountry, flight.DestinationCountry, flight.DepartureDate.ToString("yyyy-MM-dd HH:mm"), flight.DepartureAirport, flight.ArrivalAirport, flight.Class, flight.Price);
                }
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
    }
}