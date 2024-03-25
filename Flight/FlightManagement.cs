using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Flights
{
    public  class FlightManagement
    {
        private List<Flight> flights;

       public FlightManagement()
      {
       flights = new List<Flight>();
      }

        public List<Flight> SearchFlights(decimal Price, string departureCountry, string destinationCountry, DateTime departureDate, string departureAirport, string arrivalAirport, FlightClass flightClass)
        {
            // Filter flights based on search parameters
            var filteredFlights = flights.Where(f =>
                f.Price <= Price &&
                f.DepartureCountry == departureCountry &&
                f.DestinationCountry == destinationCountry &&
                f.DepartureDate.Date == departureDate.Date &&
                f.DepartureAirport == departureAirport &&
                f.ArrivalAirport == arrivalAirport &&
                f.Class == flightClass).ToList();

            return filteredFlights;
        }
        private static void FilterBookings()
        {
            Console.WriteLine("Filter Bookings operation selected.");
            // Add logic for filtering bookings
        }
        private static void BatchFlightUpload()
        {
            Console.WriteLine("Batch Flight Upload operation selected.");
            // Add logic for batch flight upload
        }
    }

}
