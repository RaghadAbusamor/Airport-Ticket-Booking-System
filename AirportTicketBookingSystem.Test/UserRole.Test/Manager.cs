using AirportTicketBookingSystem.UserRole;

namespace AirportTicketBookingSystem.Tests.UserRole
{
    public class ManagerTests
    {
        [Theory]
        [InlineData("John Doe", 123)]
        [InlineData("Peter Smith", 789)]
        public void Manager_Constructor_SetsPropertiesCorrectly(string name, int id)
        {
            // Act
            var manager = new Manager(name, id);

            // Assert
            Assert.Equal(name, manager.Name);
            Assert.Equal(id, manager.Id);

        }
    }
}
