using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
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
            return View(adminView);
        }

        public ActionResult EditFlight(int Id)
        {
            adminView.flight = new Flight();
            adminView.flights = new List<Flight>();
            Flight temp = (from x in dal.Flights where x.Id == Id select x).FirstOrDefault();
            adminView.flight = temp;
            return View(adminView);
        }

        public ActionResult SubmitFlight(Flight flight)
        {
            adminView.flight = flight;
            //Check the id of the flight
            bool sameId = (from x in dal.Flights where x.Id == flight.Id select x).Any();
            if (ModelState.IsValid && !sameId)
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
            if (sameId)
                ViewBag.SameIdFlight = "The flight Id is already exists!";
            return View("AddFlight");
        }

        public ActionResult SubmitEdition(Flight flight)
        {
            if (ModelState.IsValid)
            {
                dal.Flights.AddOrUpdate(flight);
                dal.SaveChanges();
                adminView.flight = flight;

                adminView.admin = new Admin();
                adminView.admin = (Admin)Session["Admin"];
                ViewBag.AddedFlight = "You have changed a flight succefully!";

                List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
                adminView.flights = flights;
                return View("adminPanel", adminView);
            }
            return View("EditFlight/" + flight.Id);
        }
    }
}