﻿using AirportTicketBookingSystem.Flights;

namespace AirportTicketBookingSystem.UserRole
{
    // Passenger class inheriting from User
    public class Passenger : User
    {
        public string PhoneNumber { get; set; }
        public string Passport { get; set; }
        public List<FlightData> BookedFlights { get; private set; }

        public Passenger(string name, int id, string phoneNumber, string passport) : base(name, id)
        {
            PhoneNumber = phoneNumber;
            Passport = passport;
            BookedFlights = new List<FlightData>();
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, PhoneNumber: {PhoneNumber}, PassportNumber: {Passport}";
        }
    }
}