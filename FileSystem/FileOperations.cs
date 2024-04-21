using CsvHelper;
using System.Globalization;

namespace AirportTicketBookingSystem.FileSystem
{
    public static class FileOperations
    {
        public static async Task<List<T>> ReadFromCSVAsync<T>(string filePath)
        {
            List<T> records = new List<T>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await csv.ReadAsync();
                csv.ReadHeader();

                while (await csv.ReadAsync())
                {
                    T record = csv.GetRecord<T>();
                    records.Add(record);
                }
            }

            return records;
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