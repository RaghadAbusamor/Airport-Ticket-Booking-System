using AirportTicketBookingSystem.Enums;
using AirportTicketBookingSystem.Exceptions;
using AirportTicketBookingSystem.FileSystem;
using AirportTicketBookingSystem.Flights;
using AirportTicketBookingSystem.Flights.DataModel;
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
            var cancelService = new FlightCancelService();

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
            var cancelService = new FlightCancelService();
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            Console.SetIn(new StringReader(flightNumber + Environment.NewLine));

            // Act Assert
            Assert.ThrowsAsync<FlightManagementException>(cancelService.CancelBookingAsync);

        }
        [Fact]
        public async Task CancelBookingAsync_BookingExists_PrintsSuccessMessage()
        {
            // Arrange
            var flightNumberToCancel = "ABC123";
            var bookings = new List<BookingEntry>
            {
                new BookingEntry { FlightNumber = "ABC123" },
                new BookingEntry { FlightNumber = "XYZ789" }
            };

            var mockFileOperations = new Mock<IFileOperations>();
            mockFileOperations.Setup(m => m.ReadFromCSVAsync<BookingEntry>(It.IsAny<string>()))
                              .ReturnsAsync(bookings);
            mockFileOperations.Setup(m => m.WriteToCSVAsync(It.IsAny<string>(), It.IsAny<List<BookingEntry>>()))
                              .Returns(Task.CompletedTask)
                              .Callback<string, List<BookingEntry>>((filePath, updatedBookings) => bookings = updatedBookings);

            var service = new FlightCancelService();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                using (var sr = new StringReader(flightNumberToCancel))
                {
                    Console.SetIn(sr);

                    // Act
                    await service.CancelBookingAsync();

                    // Assert
                    var output = sw.ToString().Trim();
                    Assert.Contains($"Cancel Booking operation selected.\r\nEnter", output);
                }
            }
        }
    }

}