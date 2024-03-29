using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enums;
namespace AirportTicketBookingSystem.Flights
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public FlightClass Class { get; set; }
        public decimal Price { get; set; }

        public Flight(string flightNumber, string departureCountry, string destinationCountry,
                      DateTime departureDate, string departureAirport, string arrivalAirport,
                      FlightClass flightClass, decimal price)
        {
            FlightNumber = flightNumber;
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            Class = flightClass;
            Price = price;
        }
        public void Tostring()
        {
            Console.WriteLine( $"Flight Number: {FlightNumber}, Departure Country: {DepartureCountry}, Destination Country: {DestinationCountry}, " +
                   $"Departure Date: {DepartureDate}, Departure Airport: {DepartureAirport}, Arrival Airport: {ArrivalAirport}, " +
                   $"Class: {Class}, Price: {Price}");
        }
    }
}

