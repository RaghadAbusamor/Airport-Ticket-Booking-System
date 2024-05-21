using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.FileSystem;


namespace AirportTicketBookingSystem.Flights.FlightServices
{
    public class FlightCancelService
    {
        private const string PassengersFlightsFile = "C:\\Users\\ragha\\OneDrive\\Desktop\\FTS-Internship\\AirportTicketBookingSystem\\AirportTicketBookingSystem\\CSVFiles\\PassengersFlights.csv";
        private readonly IFileOperations _fileOperations;
        public FlightCancelService(IFileOperations fileOperations)
        {
            _fileOperations = fileOperations;
        }
        public async Task CancelBookingAsync()
        {
            Console.WriteLine("Cancel Booking operation selected.");
            Console.Write("Enter the flight number you want to cancel: ");
            string flightNumber = Console.ReadLine();

            try
            {
                List<FlightData> bookings = _fileOperations.ReadFromCSVAsync<FlightData>(PassengersFlightsFile).Result;
                var selectedBooking = FlightManagementService.FindBookingByFlightNumber(bookings, flightNumber);

                if (selectedBooking == null)
                {
                    throw new FlightManagementException($"Booking for flight {flightNumber} not found.");
                }

                bookings.Remove(selectedBooking);

                await _fileOperations.WriteToCSVAsync(PassengersFlightsFile, bookings);

                Console.WriteLine($"Flight {flightNumber} canceled successfully.");
            }
            catch (FlightManagementException ex)
            {
                Console.WriteLine($"An error occurred while canceling the booking: {ex.Message}");
            }

        }
    }
}
