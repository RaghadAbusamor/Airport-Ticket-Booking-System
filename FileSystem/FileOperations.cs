using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    public class FileOperations
    {
        public static async Task<List<string[]>> ReadFromCSVAsync(string filePath)
        {
            List<string[]> data = new List<string[]>();

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');
                    data.Add(values);
                }
            }

            return data;
        }

        // Method to write data to CSV file asynchronously
        public async Task WriteToCSVAsync(string filePath, List<string[]> data)
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
