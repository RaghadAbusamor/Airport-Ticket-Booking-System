using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights;
using FluentAssertions;
using Moq;
using FlightClassPrice = AirportTicketBookingSystem.Flights.DataModel.FlightClassPrice;
using FlightDataModel = AirportTicketBookingSystem.Flights.DataModel.FlightData;

namespace AirportTicketBookingSystem.Test.FlightServices.Test
{
    public class BatchFlightUploadTests
    {
        [Fact]
        public void BatchFlightUploadAsync_ThrowsArgumentException_ForEmptyFile()
        {
            // Arrange
            var batchFlightUpload = new FlightManagementService();

            Console.SetIn(new StringReader(string.Empty));

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(batchFlightUpload.BatchFlightUploadAsync);
        }

        [Fact]
        public async Task BatchFlightUploadAsync_ValidData_AddsFlightsToList()
        {
            var flightData1 = new FlightDataModel { FlightNumber = "ABC123", DepartureCountry = "USA", DestinationCountry = "UK", DepartureDate = DateTime.Parse("2024-05-20"), DepartureAirport = "JFK", ArrivalAirport = "LHR", Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.Business, Price = 1000 } } };
            var flightData2 = new FlightDataModel { FlightNumber = "XYZ456", DepartureCountry = "UK", DestinationCountry = "USA", DepartureDate = DateTime.Parse("2024-05-21"), DepartureAirport = "LHR", ArrivalAirport = "JFK", Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.Economy, Price = 800 } } };

            List<FlightDataModel> flights = new List<FlightDataModel> { flightData1, flightData2 };
            // Arrange
            var mockFileOperations = new Mock<IFileOperations>();
            mockFileOperations.Setup(fo => fo.ReadFromCSVAsync<FlightDataModel>(It.IsAny<string>()))
                              .ReturnsAsync(flights);

            var flightManagementService = new FlightManagementService
            {
                FileOperations = mockFileOperations.Object,
                Flights = new List<Flights.DataModel.FlightData>()
            };

            var testFilePath = Path.GetTempFileName();
            Console.SetIn(new StringReader(testFilePath));

            // Act
            await flightManagementService.BatchFlightUploadAsync();

            // Assert
            flightManagementService.Flights.Should().HaveCount(2);
            File.Delete(testFilePath);
        }
    }
}
