using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelApp.Models;

namespace TravelApp.ViewModel
{
    public class AdminView
    {
        public Admin admin { get; set; }

        public Flight flight { get; set; }

        public List<Admin> admins { get; set; }

        public List<Flight> flights { get; set; }
    }
}