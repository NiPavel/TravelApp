using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TravelApp.Dal;
using TravelApp.Models;
using TravelApp.ViewModel;

namespace TravelApp.Controllers
{
    public class AdminPanelController : Controller
    {
        FlightsDal dal = new FlightsDal();
        AdminView adminView = new AdminView();
        // GET: AdminPanel
        public ActionResult AddFlight()
        {
            adminView.flight = new Flight();
            adminView.flights = new List<Flight>();
            return View();
        }

        public ActionResult SubmitFlight(Flight flight)
        {
            adminView.flight = flight;
            if (ModelState.IsValid)
            {
                dal.Flights.Add(flight);
                dal.SaveChanges();
                adminView.flights = dal.Flights.ToList<Flight>();

                adminView.admin = new Admin();
                adminView.admin = (Admin)Session["Admin"];
                ViewBag.AddedFlight = "You have added a flight succefully!";

                List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
                adminView.flights = flights;
                return View("adminPanel", adminView);
            }
            return View("AddFlight");
        }
    }
}