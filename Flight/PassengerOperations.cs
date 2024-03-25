using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::AirportTicketBookingSystem.Enums;
using System;

namespace AirportTicketBookingSystem.Flight
{


    namespace AirportTicketBookingSystem.UserRole
    {
        public static class PassengerOperations
        {
            public static void SelectPassengerOperation()
            {
                Console.WriteLine("Passenger Operations Menu:");
                Console.WriteLine($"1. {PassengerOperation.BookFlight}");
                Console.WriteLine($"2. {PassengerOperation.CancelBooking}");
                Console.WriteLine($"3. {PassengerOperation.EditBooking}");
                Console.WriteLine($"4. {PassengerOperation.ViewPersonalBookings}");
                Console.Write("Enter your choice: ");
                PassengerOperation choice;
                while (!Enum.TryParse(Console.ReadLine(), out choice) || !Enum.IsDefined(typeof(PassengerOperation), choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid operation.");
                    Console.Write("Enter your choice: ");
                }

                switch (choice)
                {
                    case PassengerOperation.BookFlight:
                        BookFlight();
                        break;
                    case PassengerOperation.CancelBooking:
                        CancelBooking();
                        break;
                    case PassengerOperation.EditBooking:
                        EditBooking();
                        break;
                    case PassengerOperation.ViewPersonalBookings:
                        ViewPersonalBookings();
                        break;
                }
            }

            private static void BookFlight()
            {
                Console.WriteLine("Book a Flight operation selected.");
                // Add logic for booking a flight
            }

            private static void CancelBooking()
            {
                Console.WriteLine("Cancel Booking operation selected.");
                // Add logic for canceling a booking
            }

            private static void EditBooking()
            {
                Console.WriteLine("Edit Booking operation selected.");
                // Add logic for editing a booking
            }

            private static void ViewPersonalBookings()
            {
                Console.WriteLine("View Personal Bookings operation selected.");
                // Add logic for viewing personal bookings
            }
        }
    }

}
