using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Flights.UsersOperations;

namespace AirportTicketBookingSystem.UserRole
{
    public static class UserTypeSelectionService
    {
        public static void TypeSelection()
        {
            while (true)
            {
                DisplayWelcomeMessage();
                UserChoice choice = GetUserChoice();

                switch (choice)
                {
                    case UserChoice.Manager:
                        HandleManager();
                        break;

                    case UserChoice.Passenger:
                        HandlePassenger();
                        break;

                    case UserChoice.Exit:
                        ExitProgram();
                        return;

                    default:
                        DisplayInvalidInputMessage();
                        break;
                }
            }
        }

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to the Airport Ticket Booking System!");
            Console.WriteLine("Are you a manager, a passenger, or do you want to exit?");
            Console.WriteLine("1. Manager");
            Console.WriteLine("2. Passenger");
            Console.WriteLine("3. Exit");
        }

        private static UserChoice GetUserChoice()
        {
            Console.Write("Enter your choice (1, 2, or 3): ");
            string input = Console.ReadLine().Trim();

            if (Enum.TryParse(input, out UserChoice choice) && Enum.IsDefined(typeof(UserChoice), choice))
            {
                return choice;
            }
            else
            {
                return UserChoice.Exit; // Default to Exit in case of invalid input
            }
        }

        private static void HandleManager()
        {
            Manager manager = GetManagerDetails();
            ManagerOperations managerOperation = new ManagerOperations();
            managerOperation.SelectManagerOperation(manager);
        }

        private static Manager GetManagerDetails()
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
            return new Manager(name, id);
        }

        private static void HandlePassenger()
        {
            Passenger passenger = GetPassengerDetails();
            PassengerOperations passengerOperations = new PassengerOperations();
            passengerOperations.SelectPassengerOperation(passenger);
        }

        private static Passenger GetPassengerDetails()
        {
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
            string phoneNumber = Console.ReadLine();
            return new Passenger(name, id, phoneNumber, passportNumber);
        }

        private static void DisplayInvalidInputMessage()
        {
            Console.WriteLine("Invalid input. Please enter 1 for manager, 2 for passenger, or 3 to exit.");
        }

        private static void ExitProgram()
        {
            Console.WriteLine("Exiting the Airport Ticket Booking System. Goodbye!");
        }
    }
}