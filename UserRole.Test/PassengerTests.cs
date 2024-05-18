using AirportTicketBookingSystem.UserRole;

namespace AirportTicketBookingSystem.Tests.UserRole
{
    public class PassengerTests
    {
        [Theory]
        [InlineData("John Doe", 123, "123-456-7890", "ABC12345")]
        [InlineData("Peter Smith", 789, "555-555-5555", "GHI98765")]
        public void Passenger_Constructor_SetsPropertiesCorrectly(string name, int id, string phoneNumber, string passport)
        {
            // Act
            var passenger = new Passenger(name, id, phoneNumber, passport);

            // Assert
            Assert.Equal(name, passenger.Name);
            Assert.Equal(id, passenger.Id);
            Assert.Equal(phoneNumber, passenger.PhoneNumber);
            Assert.Equal(passport, passenger.Passport);
            Assert.Empty(passenger.BookedFlights);
        }

        [Theory]
        [InlineData("John Doe", 123, "123-456-7890", "ABC12345",
            "Id: 123, Name: John Doe, PhoneNumber: 123-456-7890, PassportNumber: ABC12345")]
        public void Passenger_ToString_ReturnsFormattedString(string name, int id, string phoneNumber, string passport, string expectedString)
        {
            // Act
            var passenger = new Passenger(name, id, phoneNumber, passport);
            string passengerString = passenger.ToString();

            // Assert
            Assert.Equal(expectedString, passengerString);
        }
    }
}

