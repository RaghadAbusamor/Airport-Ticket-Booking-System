using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Flights;

namespace AirportTicketBookingSystem.UserRole
{
    public static class UserTypeSelection
    {
        public static void TypeSelection()
        {
            Console.WriteLine("Welcome to the Airport Ticket Booking System!");
            Console.WriteLine("Are you a manager or a passenger?");
            Console.WriteLine("1. Manager");
            Console.WriteLine("2. Passenger");
            Console.Write("Enter your choice (1 or 2): ");
            UserType userType;
            while (!Enum.TryParse(Console.ReadLine(), out userType) || (userType != UserType.Manager && userType != UserType.Passenger))
            {
                Console.WriteLine("Invalid input. Please enter 1 for manager or 2 for passenger.");
                Console.Write("Enter your choice (1 or 2): ");
            }

            if (userType == UserType.Manager)
            {
                Console.WriteLine("You have selected Manager.");
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                Console.Write("Enter your ID: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Invalid input. Please enter a valid ID.");
                    Console.Write("Enter your ID: ");
                }
                var manager = new Manager(name, id);
                ManagerOperations managerOperation = new ManagerOperations();
                managerOperation.SelectManagerOperation(manager);
            }
            else if (userType == UserType.Passenger)
            {
                // Passenger selected
                Console.WriteLine("You have selected Passenger.");
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                Console.Write("Enter your ID: ");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Invalid input. Please enter a valid ID.");
                    Console.Write("Enter your ID: ");
                }

                Console.Write("Enter your passport number: ");
                string passportNumber = Console.ReadLine();

                Console.Write("Enter your Phone Number: ");
                string PhoneNumber = Console.ReadLine();
                // Create a new passenger object with the entered details
                var passenger = new Passenger(name, id, PhoneNumber, passportNumber);
                PassengerOperations passengerOperations = new PassengerOperations();
                // Display the passenger operations
                passengerOperations.SelectPassengerOperation(passenger);
            }
        }
    }
}