using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelApp.Models;
using TravelApp.Dal;
using TravelApp.ViewModel;

namespace TravelApp.Controllers
{
    public class AdminSignUpController : Controller
    {
        AdminDal dal = new AdminDal();
        FlightsDal fdal = new FlightsDal();
        AdminView adminView = new AdminView();

        public ActionResult newAdmin()
        {
            adminView.admin = new Admin();
            adminView.admins = new List<Admin>();
            return View(adminView);
        }

        [HttpPost]
        public ActionResult submitAdmin(Admin admin)
        {
            adminView.admin = admin;
            if(ModelState.IsValid)
            {
                dal.Admins.Add(admin);
                dal.SaveChanges();
                adminView.admins = dal.Admins.ToList<Admin>();
                adminView.flights = fdal.Flights.ToList<Flight>();
                Session["AdminIn"] = true;
                return View("adminPanel", adminView);
            }
            return View("newAdmin", adminView);
        }
    }
}