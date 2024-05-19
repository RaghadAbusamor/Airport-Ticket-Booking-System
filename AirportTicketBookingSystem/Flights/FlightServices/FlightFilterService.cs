using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.FileSystem;

namespace AirportTicketBookingSystem.Flights
{
    public class FlightFilterService
    {
        private const string Booking = "C:\\Users\\ragha\\OneDrive\\Desktop\\FTS-Internship\\AirportTicketBookingSystem\\CSVFiles\\PassengersFlights.csv";
        private List<FlightData> _flights;

        public async Task FilterBookingsAsync()
        {
            try
            {
                _flights = await FileOperations.ReadFromCSVAsync<FlightData>(Booking);

                Console.WriteLine("Enter parameter and value to filter flights (e.g., 'class' = 'busniss'):");
                Console.Write("Enter parameter: ");
                string parameterString = Console.ReadLine();
                Console.Write("Enter value: ");
                string value = Console.ReadLine();

                if (!Enum.TryParse(parameterString, true, out FilterParameter parameter))
                {
                    Console.WriteLine("Invalid parameter.");
                    return;
                }

                Func<FlightData, bool> filterPredicate = GetFilterPredicate(parameter, value);

                if (filterPredicate == null)
                {
                    Console.WriteLine("Invalid parameter value.");
                    return;
                }

                List<FlightData> filteredFlights = _flights.Where(filterPredicate).ToList();
                FlightManagementService.DisplayFilteredFlights(filteredFlights);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public Func<FlightData, bool> GetFilterPredicate(FilterParameter parameter, string value)
        {
            switch (parameter)
            {
                case FilterParameter.FlightNumber:
                    return f => f.FlightNumber.Equals(value, StringComparison.OrdinalIgnoreCase);
                case FilterParameter.Price:
                    if (decimal.TryParse(value, out decimal price))
                        return f => f.Prices.Any(p => p.Price == price);
                    else
                        return null;
                case FilterParameter.DepartureCountry:
                    return f => f.DepartureCountry.Equals(value, StringComparison.OrdinalIgnoreCase);
                case FilterParameter.DestinationCountry:
                    return f => f.DestinationCountry.Equals(value, StringComparison.OrdinalIgnoreCase);
                case FilterParameter.DepartureDate:
                    if (DateTime.TryParse(value, out DateTime departureDate))
                        return f => f.DepartureDate.Date == departureDate.Date;
                    else
                        return null;
                case FilterParameter.DepartureAirport:
                    return f => f.DepartureAirport.Equals(value, StringComparison.OrdinalIgnoreCase);
                case FilterParameter.ArrivalAirport:
                    return f => f.ArrivalAirport.Equals(value, StringComparison.OrdinalIgnoreCase);
                case FilterParameter.Class:
                    if (Enum.TryParse(value, true, out FlightClass flightClass))
                        return f => f.Prices.Any(p => p.Class == flightClass);
                    else
                        return null;
                default:
                    return null;
            }
        }
    }
}
