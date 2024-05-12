using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights.DataModel;

namespace AirportTicketBookingSystem.Flights.FlightServices
{
    public class FlightEditService
    {
        private const string PassengersFlightsFile = "C:\\Users\\ragha\\OneDrive\\Desktop\\FTS-Internship\\AirportTicketBookingSystem\\CSVFiles\\PassengersFlights.csv";

        public async Task EditBookingAsync()
        {
            try
            {
                Console.WriteLine("Edit Booking operation selected.");
                Console.Write("Enter the number of bookings you want to edit: ");
                string flightNumber = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(flightNumber))
                {
                    throw new ArgumentException("Flight number cannot be empty.");
                }

                List<FlightData> bookings = await FileOperations.ReadFromCSVAsync<FlightData>(PassengersFlightsFile);
                var selectedBooking = bookings.FirstOrDefault(b => b.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));

                if (selectedBooking == null)
                {
                    throw new FlightManagementException($"Booking for flight {flightNumber} not found.");
                }

                var newClass = GetNewBookingClass();
                selectedBooking.Prices.ForEach(price => price.Class = newClass);

                Console.WriteLine($"Booking for flight {flightNumber} updated to class {newClass}.");

                await FileOperations.WriteToCSVAsync(PassengersFlightsFile, bookings);
            }
            catch (FlightManagementException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private FlightClass GetNewBookingClass()
        {
            while (true)
            {
                Console.Write($"Enter the new class for the booking: ");
                string newClassInput = Console.ReadLine();

                if (Enum.TryParse(newClassInput, true, out FlightClass newClass))
                {
                    return newClass;
                }
                else
                {
                    Console.WriteLine($"Invalid class: {newClassInput}. Please enter a valid class.");
                }
            }
        }
    }
}
