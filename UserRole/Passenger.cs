using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRole;
using Flights;
namespace AirportTicketBookingSystem.UserRole
{
    // Passenger class inheriting from User
    public class Passenger : User
{
    public string PhoneNumber { get; set; }
    public string Passport { get; set; }
    public List<Flight> BookedFlights { get; private set; }

    public Passenger(string name, int id, string phoneNumber, string passport) : base(name, id)
    {
        PhoneNumber = phoneNumber;
        Passport = passport;
        BookedFlights = new List<Flight>();
    }
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, PhoneNumber: {PhoneNumber}, PassportNumber: {Passport}";
    }
}
}
