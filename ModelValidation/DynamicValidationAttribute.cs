using AirportTicketBookingSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.ModelValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DynamicValidationAttribute : ValidationAttribute
    {
        private readonly string _allowedRange;
        private readonly bool _isRequired;
        private readonly string _type;

        public DynamicValidationAttribute(string type, bool isRequired, string allowedRange)
            : base("{0} field validation failed.")
        {
            _type = type;
            _isRequired = isRequired;
            _allowedRange = allowedRange;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_isRequired && (value == null || (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))))
            {
                return new ValidationResult(ErrorMessageString);
            }

            if (_type == "DateTime" && _allowedRange == "today → future")
            {
                if (!(value is DateTime dateValue))
                    return new ValidationResult($"Invalid date format for {validationContext.DisplayName}.");

                if (dateValue.Date < DateTime.Today)
                    return new ValidationResult($"{validationContext.DisplayName} must be today or in the future.");
            }
            if (_type == "FlightClass")
            {
                if (!(value is FlightClass))
                    return new ValidationResult($"{validationContext.DisplayName} must be of type FlightClass.");
            }

            return ValidationResult.Success;
        }
    }
}