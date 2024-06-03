using CsvHelper;
using System.Globalization;

namespace AirportTicketBookingSystem.FileSystem
{
    public class FileOperations
    {
        public static Task<List<T>> ReadFromCSVAsync<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return Task.FromResult(csv.GetRecords<T>().ToList());
            }
        }

        public static async Task WriteToCSVAsync<T>(string filePath, T data)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                await csv.WriteRecordsAsync(new List<T> { data });
            }
        }

    }
}
