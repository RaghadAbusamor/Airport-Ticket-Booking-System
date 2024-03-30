
using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.ModelValidation;

namespace AirportTicketBookingSystem.Flights
{
    public class Flight
    {

        [DynamicValidation("string", true, "",ErrorMessage = "Flight number is required")]
        public string FlightNumber { get; set; }

        [DynamicValidation("string", true, "", ErrorMessage = "Departure country is required")]
        public string DepartureCountry { get; set; }

        [DynamicValidation("string", true, "", ErrorMessage = "     ")]
        public string DestinationCountry { get; set; }
        // validation using two ways: 1. DynamicValidation 2.FutureDate
        [DynamicValidation("DateTime", true, "", ErrorMessage = "Departure date is required")]
        [FutureDate(ErrorMessage = "Departure date must be today or in the future")]
        public DateTime DepartureDate { get; set; }

        [DynamicValidation("string", true, "", ErrorMessage = "Departure airport is required")]
        public string DepartureAirport { get; set; }

        [DynamicValidation("string", true, "", ErrorMessage = "Arrival airport is required")]
        public string ArrivalAirport { get; set; }

        [DynamicValidation("FlightClass", true, "", ErrorMessage = "Flight class is required")]
        public FlightClass Class { get; set; }

        [DynamicValidation("decimal", true, "", ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price can't be negative")]
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
        public override string ToString()
        {
            return $"Flight Number: {FlightNumber}, Departure Country: {DepartureCountry}, Destination Country: {DestinationCountry}, " +
                   $"Departure Date: {DepartureDate}, Departure Airport: {DepartureAirport}, Arrival Airport: {ArrivalAirport}, " +
                   $"Class: {Class}, Price: {Price}";
        }
    }
}

