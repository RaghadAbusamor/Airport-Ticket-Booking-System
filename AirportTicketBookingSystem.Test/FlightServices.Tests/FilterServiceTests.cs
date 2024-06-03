using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Flights;

namespace AirportTicketBookingSystem.Test.FlightServices.Test
{
    public class FlightFilterServiceTests
    {
        [Fact]
        public void GetFilterPredicate_InvalidParameter_ShouldReturnNull()
        {
            // Arrange
            var flightFilterService = new FlightFilterService();

            // Act
            var predicate = flightFilterService.GetFilterPredicate((FilterParameter)999, "some value");

            // Assert
            Assert.Null(predicate);
        }
        [Fact]
        public void GetFilterPredicate_InvalidPrice_ShouldReturnNull()
        {
            // Arrange
            var flightFilterService = new FlightFilterService();

            // Act
            var predicate = flightFilterService.GetFilterPredicate(FilterParameter.Price, "invalid price");

            // Assert
            Assert.Null(predicate);
        }

        [Fact]
        public void GetFilterPredicate_InvalidDate_ShouldReturnNull()
        {
            // Arrange
            var flightFilterService = new FlightFilterService();

            // Act
            var predicate = flightFilterService.GetFilterPredicate(FilterParameter.DepartureDate, "invalid date");

            // Assert
            Assert.Null(predicate);
        }
    }
}


