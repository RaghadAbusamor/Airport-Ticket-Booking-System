using System.ComponentModel.DataAnnotations;


namespace AirportTicketBookingSystem.ModelValidation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date;
            if (value == null || !DateTime.TryParse(value.ToString(), out date))
                return false;

            return date >= DateTime.Today;
        }
    }
}
