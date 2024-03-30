
using AirportTicketBookingSystem.Flights;
using AirportTicketBookingSystem.Enums;

namespace AirportTicketBookingSystem.FileSystem
{
    public static class FileOperations
    {
        public static async Task<List<Flight>> ReadFlightsFromCSVAsync(string filePath)
        {
            List<Flight> flights = new List<Flight>();

            using (var reader = new StreamReader(filePath))
            {
                // Read the header to skip it
                await reader.ReadLineAsync();

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    // Parse CSV values into Flight properties
                    string flightNumber = values[0];
                    string departureCountry = values[1];
                    string destinationCountry = values[2];
                    DateTime departureDate = DateTime.Parse(values[3]); // Assuming date is in a valid format
                    string departureAirport = values[4];
                    string arrivalAirport = values[5];
                    FlightClass flightClass = Enum.Parse<FlightClass>(values[6]); // Assuming flight class is in correct format
                    decimal price = decimal.Parse(values[7]); // Assuming price is in correct format

                    // Create a new Flight object and add it to the list
                    Flight flight = new Flight(flightNumber, departureCountry, destinationCountry, departureDate, departureAirport, arrivalAirport, flightClass, price);
                    flights.Add(flight);
                }
            }

            return flights;
        }

        public static async Task WriteToCSVAsync(string filePath, List<string[]> data)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var row in data)
                {
                    await writer.WriteLineAsync(string.Join(",", row));
                }
            }
        }
    }
}
