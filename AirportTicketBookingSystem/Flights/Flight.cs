using AirportTicketBookingSystem.Flight.DataModel;
using AirportTicketBookingSystem.ModelValidation;

namespace AirportTicketBookingSystem.Flights
{
    public class Flight
    {
        public Flight(FlightData data)
        {
            FlightNumber = data.FlightNumber;
            DepartureCountry = data.DepartureCountry;
            DestinationCountry = data.DestinationCountry;
            DepartureDate = data.DepartureDate;
            DepartureAirport = data.DepartureAirport;
            ArrivalAirport = data.ArrivalAirport;
            Prices = data.Prices;
        }

        [DynamicValidation("string", true, "", ErrorMessage = "Arrival airport is required")]
        public string ArrivalAirport { get; set; }

        [DynamicValidation("string", true, "", ErrorMessage = "Departure airport is required")]
        public string DepartureAirport { get; set; }

        [DynamicValidation("string", true, "", ErrorMessage = "Departure country is required")]
        public string DepartureCountry { get; set; }

        // validation using two ways: 1. DynamicValidation 2.FutureDate
        [DynamicValidation("DateTime", true, "", ErrorMessage = "Departure date is required")]
        [FutureDate(ErrorMessage = "Departure date must be today or in the future")]
        public DateTime DepartureDate { get; set; }

        [DynamicValidation("string", true, "", ErrorMessage = " Destination Country is required   ")]
        public string DestinationCountry { get; set; }

        [DynamicValidation("string", true, "", ErrorMessage = "Flight number is required")]
        public string FlightNumber { get; set; }

        public List<FlightClassPrice> Prices { get; set; }

        public override string ToString()
        {
            var priceInfo = string.Join(", ", Prices);
            return $"Flight Number: {FlightNumber}, Departure Country: {DepartureCountry}, Destination Country: {DestinationCountry}, " +
                   $"Departure Date: {DepartureDate}, Departure Airport: {DepartureAirport}, Arrival Airport: {ArrivalAirport}, " +
                   $"Prices: {priceInfo}";
        }
    }
}
