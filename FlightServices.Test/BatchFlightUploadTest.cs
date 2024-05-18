using AirportTicketBookingSystem.Flights;

namespace AirportTicketBookingSystem.Test.FlightServices.Test
{
    public class BatchFlightUploadTest
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

    }
}
