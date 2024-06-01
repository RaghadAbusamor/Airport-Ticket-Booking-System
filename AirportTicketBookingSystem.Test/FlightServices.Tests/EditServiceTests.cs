using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights.DataModel;
using AirportTicketBookingSystem.Flights.FlightServices;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using System.Reflection;
using FlightClassPrice = AirportTicketBookingSystem.Flights.DataModel.FlightClassPrice;


namespace AirportTicketBookingSystem.Tests
{
    public class EditServiceTests
    {
        public string PassengersFlightsFile = "C:\\Users\\ragha\\OneDrive\\Desktop\\FTS-Internship\\AirportTicketBookingSystem\\AirportTicketBookingSystem\\CSVFiles\\PassengersFlights.csv";

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
        public void GetNewBookingClass_ReturnsValidClassEdit(FlightClass expectedClass)
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
            var flightNumber = "AB123";
            var initialClass = FlightClass.Economy;
            var bookings = new List<FlightData>
            {
                new FlightData
                {
                    FlightNumber = flightNumber,
                    Prices = new List<FlightClassPrice>
                    {
                        new FlightClassPrice { Class = initialClass, Price = 200 }
                    }
                }
            };

            var fileOperationsMock = new Mock<IFileOperations>();
            fileOperationsMock.Setup(fo => fo.ReadFromCSVAsync<FlightData>(It.IsAny<string>()))
                .ReturnsAsync(bookings);

            fileOperationsMock.Setup(fo => fo.WriteToCSVAsync(It.IsAny<string>(), It.IsAny<List<FlightData>>()))
                .Returns(Task.CompletedTask);

            var flightEditService = new FlightEditService();

            // Simulate user input for the new class
            var inputReader = new StringReader(initialClass.ToString());
            Console.SetIn(inputReader);
            // Act
            await flightEditService.EditBookingAsync();
            // Assert
            var editedBooking = bookings.FirstOrDefault(b => b.FlightNumber == flightNumber);
            Assert.NotNull(editedBooking);
            Assert.All(editedBooking.Prices, price => Assert.Equal(initialClass, price.Class));

        }

    }
}
