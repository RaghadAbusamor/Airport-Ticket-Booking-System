﻿using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.UserRole;

namespace AirportTicketBookingSystem.Flights.UsersOperations
{
    public class ManagerOperations
    {
        public FlightManagement flightManagement = new FlightManagement();

        public void SelectManagerOperation(Manager manager)
        {
            Console.Clear();
            Console.WriteLine($"HELLO {manager.Name}, Welcome to Booking System!");

            ManagerMenu();
            Console.Write("Enter your choice: ");
            ManagerOperation choice;
            while (!Enum.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid operation.");
                Console.Write("Enter your choice: ");
            }

            switch (choice)
            {
                case ManagerOperation.FilterBookings:
                    flightManagement.FilterBookings();
                    break;

                case ManagerOperation.BatchFlightUpload:
                    flightManagement.BatchFlightUploadAsync();
                    break;

                case ManagerOperation.Exit:
                    flightManagement.Exit();
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
            Console.Clear();
        }


        private static void ManagerMenu()
        {
            Console.WriteLine("Manager Operations Menu:");
            Console.WriteLine($"1. {ManagerOperation.FilterBookings}");
            Console.WriteLine($"2. {ManagerOperation.BatchFlightUpload}");
            Console.WriteLine($"3. {ManagerOperation.Exit}");
        }
    }
}