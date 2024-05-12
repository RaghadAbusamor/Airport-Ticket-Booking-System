using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights.DataModel;


namespace AirportTicketBookingSystem.Flights
{
    public class FlightManagementService
    {
        private const string PassengersFlightsFile = "C:\\Users\\ragha\\OneDrive\\Desktop\\FTS-Internship\\AirportTicketBookingSystem\\CSVFiles\\PassengersFlights.csv";
        public List<Flight> Flights { get; set; }
        public async Task BatchFlightUploadAsync()
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

                List<Flight> newFlights = await FileOperations.ReadFromCSVAsync<Flight>(filePath);
                Flights.AddRange(newFlights);

                Console.WriteLine("Flights successfully uploaded.");
            }
            catch (FlightManagementException ex)
            {
                Console.WriteLine($"Flight management error: {ex.Message}");
            }
        }
        public async Task BookFlightAsync(int passengerId)
        {
            Console.WriteLine("Book a Flight operation selected.");
            Console.Write("Enter the number of flights you want to book: ");
            string? flightNumber = Console.ReadLine();

            var selectedFlight = Flights.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
            if (selectedFlight == null)
            {
                Console.WriteLine($"Flight with number {flightNumber} not found.");
                return;
            }

            var bookingEntry = new BookingEntry
            {
                PassengerId = passengerId,
                FlightNumber = selectedFlight.FlightNumber,
                DepartureCountry = selectedFlight.DepartureCountry,
                DestinationCountry = selectedFlight.DestinationCountry,
                DepartureDate = selectedFlight.DepartureDate,
                DepartureAirport = selectedFlight.DepartureAirport,
                ArrivalAirport = selectedFlight.ArrivalAirport,
                Prices = selectedFlight.Prices
            };

            await FileOperations.WriteToCSVAsync(PassengersFlightsFile, bookingEntry);
            Console.WriteLine($"Flight {flightNumber} added to your bookings.");
        }
        public void Exit()
        {
            Console.WriteLine("Exiting, BYEEEEEE");
            Environment.Exit(0);
        }
        public void ListAllFlights(int passengerId)
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
                    BookFlightAsync(passengerId);
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

        public void ViewPersonalBookings(int passengerId)
        {
            try
            {
                Console.WriteLine("View Personal Booking:\n");
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", "Flight", "Departure", "Destination", "Departure Date", "Departure Airport", "Arrival Airport", "Class", "Price");
                Console.WriteLine(new string('-', 120));

                List<BookingEntry> allBookings = FileOperations.ReadFromCSVAsync<BookingEntry>(PassengersFlightsFile).Result;
                var personalBookings = allBookings.Where(f => f.PassengerId == passengerId).ToList();

                if (personalBookings.Count == 0)
                {
                    throw new FlightManagementException("No bookings found for the specified passenger.");
                }

                DisplayFlights(personalBookings);
            }
            catch (FlightManagementException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static void DisplayFlights<T>(List<T> entities)
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(entity.ToString());
            }
        }
        public static void DisplayFilteredFlights<T>(List<T> entities)
        {
            Console.WriteLine("Filtered Flights:");
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-15} {5,-15} {6,-10} {7,-10}", "Flight Number", "Departure Country", "Destination Country", "Departure Date", "Departure Airport", "Arrival Airport", "Class", "Price");
            Console.WriteLine(new string('-', 120));

            DisplayFlights(entities);
        }
        public static Flight FindBookingByFlightNumber(List<Flight> bookings, string flightNumber)
        {
            return bookings.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
        }
    }
}
