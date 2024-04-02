using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.UserRole;

namespace AirportTicketBookingSystem.Flights
{
    public class PassengerOperations
    {
        public FlightManagement flightManagement = new FlightManagement();

        public void SelectPassengerOperation(Passenger passenger)
        {
            Console.Clear();
            Console.WriteLine($"HELLO {passenger.Name}, Welcome to Booking System!");

            while (true)
            {
                PasengerMenu();
                Console.Write("Enter your choice: ");
                PassengerOperation choice;
                while (!Enum.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid operation.");
                    Console.Write("Enter your choice: ");
                }

                switch (choice)
                {
                    case PassengerOperation.BookFlight:
                        flightManagement.BookFlight();
                        break;

                    case PassengerOperation.ListAllFlights:
                        flightManagement.ListAllFlights();
                        break;

                    case PassengerOperation.CancelBooking:
                        flightManagement.CancelBooking();
                        break;

                    case PassengerOperation.EditBooking:
                        flightManagement.EditBooking();
                        break;

                    case PassengerOperation.ViewPersonalBookings:
                        flightManagement.ViewPersonalBookings();
                        break;

                    case PassengerOperation.SearchBooking:
                        flightManagement.FilterBookings();
                        break;

                    case PassengerOperation.Exit:

                        flightManagement.Exit();
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        private static void PasengerMenu()
        {
            Console.WriteLine("Passenger Operations Menu:");
            Console.WriteLine($"1. {PassengerOperation.BookFlight}");
            Console.WriteLine($"2. {PassengerOperation.ListAllFlights}");
            Console.WriteLine($"3. {PassengerOperation.CancelBooking}");
            Console.WriteLine($"4. {PassengerOperation.EditBooking}");
            Console.WriteLine($"5. {PassengerOperation.ViewPersonalBookings}");
            Console.WriteLine($"6. {PassengerOperation.SearchBooking}");
            Console.WriteLine($"7. Exit");
        }
    }
}