using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.UserRole
{
    // Manager class inheriting from User
    public class Manager : User
    {
        public Manager(string name, int id) : base(name, id)
        {
            // Initialize additional properties if needed
        }
    }
}
