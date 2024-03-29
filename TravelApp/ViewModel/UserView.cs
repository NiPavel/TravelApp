﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelApp.Models;
using TwoFactorAuth;

namespace TravelApp.ViewModel
{
    public class UserView
    {
        public User user { get; set; }

        public List<User> users { get; set; }

        public Flight flight { get; set; }
        public List<Flight> flights { get; set; }
        
        public Order order { get; set; }
        public List<Order> orders { get; set; }

        public Payment payment { get; set; }

        public List<Payment> payments { get; set; }

        public List<Flight> oneWay { get; set; } 
        public List<Flight> twoWay { get; set; } 

        public List<List<Flight>> choosenFlights { get; set;}
        public List<Order> boughtFlights { get; set;}


    }
}