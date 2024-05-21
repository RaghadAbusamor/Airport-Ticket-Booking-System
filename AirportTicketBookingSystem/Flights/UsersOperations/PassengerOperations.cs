using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.UserRole;

namespace AirportTicketBookingSystem.Flights.UsersOperations
{
    public class PassengerOperations
    {
        private readonly FlightManagement _flightManagement;

        public PassengerOperations()
        {
            _flightManagement = new FlightManagement();
        }

        public async Task SelectPassengerOperation(Passenger passenger)
        {
            Console.Clear();
            Console.WriteLine($"Hello {passenger.Name}, Welcome to the Booking System!");

            while (true)
            {
                DisplayPassengerMenu();
                if (!Enum.TryParse(Console.ReadLine(), out PassengerOperation choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid operation.");
                    continue;
                }

                await ProcessPassengerOperation(choice, passenger.Id);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        private async Task ProcessPassengerOperation(PassengerOperation choice, int passengerId)
        {
            switch (choice)
            {
                case PassengerOperation.BookFlight:
                    await _flightManagement.BookFlightAsync(passengerId);
                    break;

                case PassengerOperation.ListAllFlights:
                    _flightManagement.ListAllFlights(passengerId);
                    break;

                case PassengerOperation.CancelBooking:
                    await _flightManagement.CancelBookingAsync();
                    break;

                case PassengerOperation.EditBooking:
                    _flightManagement.EditBooking();
                    break;

                case PassengerOperation.ViewPersonalBookings:
                    _flightManagement.ViewPersonalBookings(passengerId);
                    break;

                case PassengerOperation.SearchBooking:
                    _flightManagement.Search();
                    break;

                case PassengerOperation.Exit:
                    _flightManagement.Exit();
                    break;
            }
        }

        private static void DisplayPassengerMenu()
        {
            Console.WriteLine("Passenger Operations Menu:");
            foreach (PassengerOperation operation in Enum.GetValues(typeof(PassengerOperation)))
            {
                Console.WriteLine($"{(int)operation}. {operation}");
            }
        }
    }
}