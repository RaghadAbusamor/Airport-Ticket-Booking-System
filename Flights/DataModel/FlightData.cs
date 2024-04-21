namespace AirportTicketBookingSystem.Flights.DataModel
{
    public class FlightData
    {
        public string FlightNumber { get; set; }
        public string DepartureCountry { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public List<FlightClassPrice> Prices { get; set; }
    }
}
