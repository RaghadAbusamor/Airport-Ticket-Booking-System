using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights.DataModel;
using AirportTicketBookingSystem.Flights.FlightServices;
using Moq;

namespace AirportTicketBookingSystem.Tests
{
    public class EditServiceTests
    {

        [Fact]
        public void EditBookingAsync_ThrowsArgumentException_ForEmptyFlightNumber()
        {
            // Arrange
            var editService = new FlightEditService();

            Console.SetIn(new StringReader(string.Empty));

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(editService.EditBookingAsync);
        }

        [Fact]
        public void EditBookingAsync_WhenBookingNotFound_ShouldThrowFlightManagementException()
        {
            // Arrange
            const string flightNumber = "XYZ789";
            var bookings = new List<FlightData>();
            var mockedFileOperations = new Mock<IFileOperations>();
            mockedFileOperations.Setup(f => f.ReadFromCSVAsync<FlightData>(It.IsAny<string>())).ReturnsAsync(bookings);
            var flightEditService = new FlightEditService();
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            Console.SetIn(new StringReader(flightNumber + Environment.NewLine));

            // Act Assert
            Assert.ThrowsAsync<FlightManagementException>(flightEditService.EditBookingAsync);

        }


        [Theory]
        [InlineData(FlightClass.Economy)]
        [InlineData(FlightClass.Business)]
        [InlineData(FlightClass.FirstClass)]
        public void GetNewBookingClass_ReturnsValidClass(FlightClass expectedClass)
        {
            // Arrange
            var editService = new FlightEditService();
            string validClassInput = expectedClass.ToString().ToLower();
            Console.SetIn(new StringReader(validClassInput));
            // Act
            var newClass = editService.GetNewBookingClass();

            // Assert
            Assert.Equal(expectedClass, newClass);
        }

    }
}
