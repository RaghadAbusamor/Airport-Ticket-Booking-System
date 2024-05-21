namespace AirportTicketBookingSystem.Test.FlightServices.Tests
{
    public class FlightFilterServiceTests
    {
        [Fact]
        public async Task FilterBookingsAsync_InvalidParameter_ReturnsErrorMessage()
        {
            // Arrange
            var filterService = new FlightFilterService();
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            Console.SetIn(new StringReader("invalid\nvalue\n"));

            // Act
            await filterService.FilterBookingsAsync();

            // Assert
            Assert.Contains("Invalid parameter.", consoleOutput.ToString());
        }

        [Fact]
        public async Task FilterBookingsAsync_ValidParameterInvalidValue_ReturnsErrorMessage()
        {
            // Arrange
            var filterService = new FlightFilterService();
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            Console.SetIn(new StringReader("class\ninvalid\n"));

            // Act
            await filterService.FilterBookingsAsync();

            // Assert
            Assert.Contains("Invalid parameter value.", consoleOutput.ToString());
        }
    }
}
