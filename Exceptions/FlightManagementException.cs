using System;

namespace AirportTicketBookingSystem.Exceptions
{
    public class FlightManagementException : Exception
    {
        public FlightManagementException() { }

        public FlightManagementException(string message) : base(message) { }

    }
}
