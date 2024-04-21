using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.ModelValidation;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Flights.DataModel
{
    public class FlightClassPrice
    {
        public FlightClass Class { get; set; }

        [DynamicValidation("decimal", true, "", ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to zero")]
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Class}: {Price}";
        }
    }
}
