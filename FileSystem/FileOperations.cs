using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using AirportTicketBookingSystem.Flights;

namespace AirportTicketBookingSystem.FileSystem
{ 
public static class FileOperations
{
    public static async Task<List<Flight>> ReadFlightsFromCSVAsync(string filePath)
    {
        List<Flight> flights = new List<Flight>();

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Skip header record
            await csv.ReadAsync();
            csv.ReadHeader();

            while (await csv.ReadAsync())
            {
                Flight flight = csv.GetRecord<Flight>();
                flights.Add(flight);
            }
        }

        return flights;
    }
}
}