using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights;
using AirportTicketBookingSystem.Flights.FlightServices;
using Moq;

namespace AirportTicketBookingSystem.Tests
{
    public class FlightCancelServiceTests
    {
        [Fact]
        public void CancelBookingAsync_ThrowsArgumentException_ForEmptyFlightNumber()
        {
            // Arrange
            var cancelService = new FlightCancelService();

            Console.SetIn(new StringReader(string.Empty));

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(cancelService.CancelBookingAsync);
        }


        [Fact]
        public void CancelBookingAsync_WhenBookingNotFound_ShouldThrowFlightManagementException()
        {
            // Arrange
            const string flightNumber = "XYZ789";
            var bookings = new List<FlightData>();
            var mockedFileOperations = new Mock<IFileOperations>();
            mockedFileOperations.Setup(f => f.ReadFromCSVAsync<FlightData>(It.IsAny<string>())).ReturnsAsync(bookings);
            var cancelService = new FlightCancelService();
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            Console.SetIn(new StringReader(flightNumber + Environment.NewLine));

            // Act Assert
            Assert.ThrowsAsync<FlightManagementException>(cancelService.CancelBookingAsync);

        }
    }
}
