﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelApp.Models;

namespace TravelApp.ViewModel
{
    public class AdminView
    {
        public Admin admin { get; set; }

        public List<Admin> admins { get; set; }
    }
}