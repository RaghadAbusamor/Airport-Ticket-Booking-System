using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights.FlightServices;
using FluentAssertions;
using Moq;
using FlightClassPrice = AirportTicketBookingSystem.Flights.DataModel.FlightClassPrice;
using FlightDataModel = AirportTicketBookingSystem.Flights.DataModel.FlightData;

namespace AirportTicketBookingSystem.Tests
{
    public class FlightCancelServiceTests
    {
        [Fact]
        public void CancelBookingAsync_ThrowsArgumentException_ForEmptyFlightNumber()
        {
            // Arrange
            var mockedFileOperations = new Mock<IFileOperations>();
            var cancelService = new FlightCancelService(mockedFileOperations.Object);

            Console.SetIn(new StringReader(string.Empty));

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(cancelService.CancelBookingAsync);
        }


        [Fact]
        public void CancelBookingAsync_WhenBookingNotFound_ShouldThrowFlightManagementException()
        {
            // Arrange
            string flightNumber = "XYZ789";
            var bookings = new List<FlightDataModel>();
            var mockedFileOperations = new Mock<IFileOperations>();
            mockedFileOperations.Setup(f => f.ReadFromCSVAsync<FlightDataModel>(It.IsAny<string>())).ReturnsAsync(bookings);
            var cancelService = new FlightCancelService(mockedFileOperations.Object);
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            Console.SetIn(new StringReader(flightNumber + Environment.NewLine));

            // Act Assert
            Assert.ThrowsAsync<FlightManagementException>(cancelService.CancelBookingAsync);

        }

        [Fact]
        public async Task CancelBookingAsync_WhenBookingFound_ShouldRemoveBooking1()
        {
            // Arrange
            string flightNumber = "ABC123";
            var flightData1 = new FlightDataModel { FlightNumber = flightNumber, DepartureCountry = "USA", DestinationCountry = "UK", DepartureDate = DateTime.Parse("2024-05-20"), DepartureAirport = "JFK", ArrivalAirport = "LHR", Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.Business, Price = 1000 } } };
            var flightData2 = new FlightDataModel { FlightNumber = "XYZ456", DepartureCountry = "UK", DestinationCountry = "USA", DepartureDate = DateTime.Parse("2024-05-21"), DepartureAirport = "LHR", ArrivalAirport = "JFK", Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.Economy, Price = 800 } } };
            List<FlightDataModel> flights = new List<FlightDataModel> { flightData1, flightData2 };

            var mockedFileOperations = new Mock<IFileOperations>();
            mockedFileOperations.Setup(f => f.ReadFromCSVAsync<FlightDataModel>(It.IsAny<string>())).ReturnsAsync(flights);
            mockedFileOperations.Setup(f => f.WriteToCSVAsync(It.IsAny<string>(), It.IsAny<List<FlightDataModel>>()))
            .Returns(Task.CompletedTask)
            .Callback<string, List<FlightDataModel>>((filePath, updatedBookings) => flights = updatedBookings);

            var cancelService = new FlightCancelService(mockedFileOperations.Object);

            // Act
            await cancelService.CancelBookingAsync();

            // Assert 
            flights.Should().HaveCount(1);
        }
    }
}


