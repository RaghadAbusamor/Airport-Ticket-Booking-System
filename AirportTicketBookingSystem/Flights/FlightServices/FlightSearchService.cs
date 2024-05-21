using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.FileSystem;


namespace AirportTicketBookingSystem.Flights.FlightServices
{
    public class FlightSearchService
    {
        private const string Booking = "C:\\Users\\ragha\\OneDrive\\Desktop\\FTS-Internship\\AirportTicketBookingSystem\\CSVFiles\\PassengersFlights.csv";
        public List<DataModel.FlightData> _flights;
        public async Task
SearchBookingAsync()
        {
            _flights = await FileOperations.ReadFromCSVAsync<DataModel.FlightData>(Booking);
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
            FlightManagementService.DisplayFilteredFlights(searchResults);
        }

        public List<DataModel.FlightData> SearchFlights(string flightNumber, string departureCountry, string destinationCountry, DateTime? departureDate, string departureAirport, string arrivalAirport, string classStr, decimal? price)
        {
            var searchResults = _flights
                .Where(f =>
                    (string.IsNullOrEmpty(flightNumber) || f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(departureCountry) || f.DepartureCountry.Equals(departureCountry, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(destinationCountry) || f.DestinationCountry.Equals(destinationCountry, StringComparison.OrdinalIgnoreCase)) &&
                    (!departureDate.HasValue || f.DepartureDate.Date == departureDate.Value.Date) &&
                    (string.IsNullOrEmpty(departureAirport) || f.DepartureAirport.Equals(departureAirport, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(arrivalAirport) || f.ArrivalAirport.Equals(arrivalAirport, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(classStr) || Enum.TryParse(classStr, true, out FlightClass flightClass) && f.Prices.Any(p => p.Class == flightClass)) &&
                    (!price.HasValue || f.Prices.Any(p => p.Price == price.Value))
                )
                .ToList();

            return searchResults;
        }
    }
}
