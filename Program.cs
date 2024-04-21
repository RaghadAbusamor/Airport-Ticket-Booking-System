using AirportTicketBookingSystem.UserRole;

namespace AirportTicketBookingSystem
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            UserTypeSelectionService.TypeSelection();
        }
    }
}