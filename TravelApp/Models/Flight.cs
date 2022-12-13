using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelApp.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public float Price { get; set; }
        public int Seats { get; set; }
    }
}