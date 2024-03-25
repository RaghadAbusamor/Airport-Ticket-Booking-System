using AirportTicketBookingSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Flight
{
    public static class ManagerOperations
    {
        public static void SelectOperation()
        {
            Console.WriteLine("Manager Operations Menu:");
            Console.WriteLine($"1. {ManagerOperation.FilterBookings}");
            Console.WriteLine($"2. {ManagerOperation.BatchFlightUpload}");
            Console.Write("Enter your choice: ");
            ManagerOperation choice;
            while (!Enum.TryParse(Console.ReadLine(), out choice) || !Enum.IsDefined(typeof(ManagerOperation), choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid operation.");
                Console.Write("Enter your choice: ");
            }

            switch (choice)
            {
                case ManagerOperation.FilterBookings:
                    FilterBookings();
                    break;
                case ManagerOperation.BatchFlightUpload:
                    BatchFlightUpload();
                    break;
            }
        }




    }
}

