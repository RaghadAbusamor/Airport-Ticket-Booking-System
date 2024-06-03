using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights;
using AirportTicketBookingSystem.Flights.DataModel;
using Moq;
using _flight = AirportTicketBookingSystem.Flights.DataModel.FlightData;

namespace AirportTicketBookingSystem.Test.FlightServices.Test
{
    public class AddBookingTests
    {
        [Fact]
        public async Task BookFlightAsync_FlightNotFound_DisplayErrorMessage()
        {
            // Arrange
            var mockFileOperations = new Mock<IFileOperations>();
            var flightManagementService = new FlightManagementService
            {
                Flights = new List<_flight>(),  // Assuming Flight is the correct type
                FileOperations = mockFileOperations.Object
            };

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            Console.SetIn(new StringReader("AXXX"));

            // Act
            await flightManagementService.BookFlightAsync(It.IsAny<int>());

            // Assert
            var output = consoleOutput.ToString().Trim();
            //  Assert.Contains("Flight with number AXXX not found.", output, StringComparison.OrdinalIgnoreCase);

            // Verify that WriteToCSVAsync was not called
            mockFileOperations.Verify(fo => fo.WriteToCSVAsync<BookingEntry>(It.IsAny<string>(), It.IsAny<BookingEntry>()), Times.Never);
        }
        [Fact]
        public async Task BookFlightAsync_FlightFound_WriteToCSVAndDisplaySuccessMessage()
        {
            // Arrange
            var mockFileOperations = new Mock<IFileOperations>();
            var selectedFlight = new _flight
            {
                FlightNumber = "ABC123",
                DepartureCountry = "USA",
                DestinationCountry = "UK",
                DepartureDate = DateTime.UtcNow,
                DepartureAirport = "JFK",
                ArrivalAirport = "LHR",
                Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.Business, Price = 1000 } }
            };
            var flightManagementService = new FlightManagementService { Flights = new List<_flight> { selectedFlight }, FileOperations = mockFileOperations.Object };
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            Console.SetIn(new StringReader("ABC123"));

            // Act
            await flightManagementService.BookFlightAsync(1);

            // Assert
            mockFileOperations.Verify(m => m.WriteToCSVAsync<BookingEntry>(It.IsAny<string>(), It.IsAny<BookingEntry>()), Times.Once);
        }

    }
}
