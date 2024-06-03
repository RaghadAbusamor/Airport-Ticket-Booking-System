using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights.DataModel;

namespace AirportTicketBookingSystem.Flights.FlightServices
{
    public class FlightCancelService
    {
        private const string PassengersFlightsFile = "C:\\Users\\ragha\\OneDrive\\Desktop\\FTS-Internship\\AirportTicketBookingSystem\\AirportTicketBookingSystem\\CSVFiles\\PassengersFlights.csv";

        public async Task CancelBookingAsync()
        {
            Console.WriteLine("Cancel Booking operation selected.");
            Console.Write("Enter the flight number you want to cancel: ");
            string flightNumber = Console.ReadLine();

            try
            {
                var bookings = await FileOperations.ReadFromCSVAsync<BookingEntry>(PassengersFlightsFile);
                if (bookings == null)
                {
                    throw new FlightManagementException("No bookings found.");
                }

                var selectedBooking = bookings.FirstOrDefault(b => b.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));

                if (selectedBooking == null)
                {
                    throw new FlightManagementException($"Booking for flight {flightNumber} not found.");
                }

                bookings.Remove(selectedBooking);

                await FileOperations.WriteToCSVAsync(PassengersFlightsFile, bookings);

                Console.WriteLine($"Flight {flightNumber} canceled successfully.");
            }
            catch (FlightManagementException ex)
            {
                Console.WriteLine($"An error occurred while canceling the booking: {ex.Message}");
            }
        }
    }
}
