using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace AirportTicketBookingSystem.Flights
{
    public class FlightManagement
    {
        public List<Flight> Flights { get; set; }
        public List<Flight> PassengerFlights { get; set; }

        public FlightManagement()
        {
            Flights = new List<Flight>();
            PassengerFlights = new List<Flight>();

            // Add a default flight
            AddDefaultFlight();
        }

        private void AddDefaultFlight()
        {
            // Add your default flight here
            Flights.Add(new Flight("ABC123", "USA", "UK", DateTime.Now.AddDays(1), "JFK", "LHR", FlightClass.Business, 1000));
        }

        public List<Flight> SearchFlights(decimal Price, string departureCountry, string destinationCountry, DateTime departureDate, string departureAirport, string arrivalAirport, FlightClass flightClass)
        {
            // Filter flights based on search parameters
            var filteredFlights = Flights.Where(f =>
                f.Price <= Price &&
                f.DepartureCountry == departureCountry &&
                f.DestinationCountry == destinationCountry &&
                f.DepartureDate.Date == departureDate.Date &&
                f.DepartureAirport == departureAirport &&
                f.ArrivalAirport == arrivalAirport &&
                f.Class == flightClass).ToList();

            return filteredFlights;
        }

        public void FilterBookings()
        {
            Console.WriteLine("Filter Bookings operation selected.");
            // Add logic for filtering bookings
        }

        public void BatchFlightUpload()
        {
            Console.WriteLine("Batch Flight Upload operation selected.");
            // Add logic for batch flight upload
        }

        public void BookFlight()
        {
            Console.WriteLine("Book a Flight operation selected.");
            Console.Write("Enter the number of flights you want to book: ");
            String? FlightNumber = Console.ReadLine();

            foreach (var flight in Flights)
            {
                if (flight.FlightNumber.Equals(FlightNumber, StringComparison.OrdinalIgnoreCase))
                {
                    Flight selectedFlight = Flights.Find(f => f.FlightNumber.Equals(flight.FlightNumber));
                    PassengerFlights.Add(selectedFlight);
                    Console.WriteLine($"Flight {FlightNumber} added to your bookings.");
                }
                else
                {
                    Console.WriteLine($"Flight with number {FlightNumber} not found.");
                }
            }
        }

        public void CancelBooking()
        {
            Console.WriteLine("Cancel Booking operation selected.");
            Console.Write("Enter the number of bookings you want to cancel: ");
            String? FlightNumber = Console.ReadLine();

            foreach (var flight in Flights)
            {
                if (flight.FlightNumber.Equals(FlightNumber, StringComparison.OrdinalIgnoreCase))
                {
                    Flight selectedFlight = Flights.Find(f => f.FlightNumber.Equals(flight.FlightNumber));
                    PassengerFlights.Remove(selectedFlight);
                    Console.WriteLine($"Flight {FlightNumber} Canceled.");
                }
                else
                {
                    Console.WriteLine($"Flight with number {FlightNumber} not found.");
                }
            }
        }

        public void EditBooking()
        {
            Console.WriteLine("Edit Booking operation selected.");
            Console.Write("Enter the number of bookings you want to edit: ");
            String? flightNumber = Console.ReadLine();

            // Find the selected booking with the entered flight number
            Flight selectedBooking = PassengerFlights.Find(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
            if (selectedBooking != null)
            {
                Console.Write($"Enter the new class for booking {flightNumber}: ");
                string newClassInput = Console.ReadLine();

                // Parse the new class input to FlightClass enum
                if (Enum.TryParse(newClassInput, true, out FlightClass newClass))
                {
                    // Update the flight class
                    selectedBooking.Class = newClass;
                    Console.WriteLine($"Booking for flight {flightNumber} updated to class {newClass}.");
                }
                else
                {
                    Console.WriteLine($"Invalid class: {newClassInput}. Please enter a valid class.");
                }
            }
            else
            {
                Console.WriteLine($"Booking for flight {flightNumber} not found.");
            }
        }


        public void ViewPersonalBookings()
        {
            Console.WriteLine("View Personal Booking:\n");
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", "Flight", "Departure", "Destination", "Departure Date", "Departure Airport", "Arrival Airport", "Class", "Price");
            Console.WriteLine(new string('-', 120));

            foreach (var flight in PassengerFlights)
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", flight.FlightNumber, flight.DepartureCountry, flight.DestinationCountry, flight.DepartureDate.ToString("yyyy-MM-dd HH:mm"), flight.DepartureAirport, flight.ArrivalAirport, flight.Class, flight.Price);
            }

        }

        public void ListAllFlights()
        {
            Console.WriteLine("Listing all flights:\n");
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", "Flight", "Departure", "Destination", "Departure Date", "Departure Airport", "Arrival Airport", "Class", "Price");
            Console.WriteLine(new string('-', 120));

            foreach (var flight in Flights)
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20} {4,-20} {5,-20} {6,-15} {7,-10}", flight.FlightNumber, flight.DepartureCountry, flight.DestinationCountry, flight.DepartureDate.ToString("yyyy-MM-dd HH:mm"), flight.DepartureAirport, flight.ArrivalAirport, flight.Class, flight.Price);
            }

            Console.WriteLine("\nDo you want to book a flight? (Y/N)");
            string choice = Console.ReadLine();

            if (choice.ToUpper() == "Y")
            {
                BookFlight();
            }
            else
            {
                Console.WriteLine("Thank you for using our flight booking service.");
            }
        }
        public void Exit()
        {
            Console.WriteLine("Exiting, BYEEEEEE");
            Environment.Exit(0);
        }
    }
}