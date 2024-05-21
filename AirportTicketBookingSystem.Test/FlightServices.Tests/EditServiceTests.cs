using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights.DataModel;
using AirportTicketBookingSystem.Flights.FlightServices;
using Moq;

namespace AirportTicketBookingSystem.Test.FlightServices.Tests
{
    public class EditServiceTests
    {
        string PassengersFlightsFile = "C:\\Users\\ragha\\OneDrive\\Desktop\\FTS-Internship\\AirportTicketBookingSystem\\AirportTicketBookingSystem\\CSVFiles\\PassengersFlights.csv";

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
        [Fact]
        public async Task EditBookingAsync_WhenBookingFound_ShouldEditBookingClass()
        {
            // Arrange
            const string flightNumber = "ABC123";
            var flightData = new FlightData
            {
                FlightNumber = flightNumber,
                DepartureCountry = "USA",
                DestinationCountry = "UK",
                DepartureDate = DateTime.Parse("2024-05-20"),
                DepartureAirport = "JFK",
                ArrivalAirport = "LHR",
                Prices = new List<FlightClassPrice> { new FlightClassPrice { Class = FlightClass.Economy, Price = 1000 } }
            };
            List<FlightData> bookings = new List<FlightData> { flightData };

            var mockedFileOperations = new Mock<IFileOperations>();
            mockedFileOperations.Setup(f => f.ReadFromCSVAsync<FlightData>(It.IsAny<string>())).ReturnsAsync(bookings);
            mockedFileOperations.Setup(f => f.WriteToCSVAsync(It.IsAny<string>(), It.IsAny<List<FlightData>>()))
                .Returns(Task.CompletedTask)
                .Callback<string, List<FlightData>>((filePath, updatedBookings) => bookings = updatedBookings);

            var editService = new FlightEditService();

            string newClassInput = "Business" + Environment.NewLine;
            string consoleInput = flightNumber + Environment.NewLine + newClassInput;
            using var stringReader = new StringReader(consoleInput);
            Console.SetIn(stringReader);

            // Act
            await editService.EditBookingAsync();

            // Assert
            mockedFileOperations.Verify(f => f.ReadFromCSVAsync<FlightData>(PassengersFlightsFile), Times.Once);
            mockedFileOperations.Verify(f => f.WriteToCSVAsync(PassengersFlightsFile, It.IsAny<List<FlightData>>()), Times.Once);

            var editedBooking = bookings.First(b => b.FlightNumber == flightNumber);
            Assert.All(editedBooking.Prices, price => Assert.Equal(FlightClass.Business, price.Class));
        }
    }
}
