using AirportTicketBookingSystem.UserRole;
using FileSystem;
using System;
using System.Threading.Tasks; // Importing the namespace for Task

namespace Airport
{
    class Program
    {
        static async Task Main(string[] args) // Change the return type to Task
        {
            UserTypeSelection.TypeSelection();

            // Call ReadFromCSVAsync and await the result
            List<string[]> data = await FileOperations.ReadFromCSVAsync("C:\\Users\\ragha\\OneDrive\\Desktop\\FTS-Internship\\AirportTicketBookingSystem\\CSVFiles\\FlightSheet.csv");

            // Print the data
            foreach (var row in data)
            {
                Console.WriteLine(string.Join(",", row));
            }
        }
    }
}
