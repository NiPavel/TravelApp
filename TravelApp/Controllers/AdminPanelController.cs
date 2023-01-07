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
        PlaneDal pdal = new PlaneDal();
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

        public ActionResult DeleteFlight(int Id)
        {
            Flight temp = (from x in dal.Flights where x.Id == Id select x).FirstOrDefault();
            dal.Flights.Remove(temp);
            dal.SaveChanges();

            adminView.admin = new Admin();
            adminView.admin = (Admin)Session["Admin"];
            ViewBag.AddedFlight = "You have deleted a flight succefully!";

            List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
            adminView.flights = flights;

            List<Plane> planes = (from x in pdal.Planes select x).ToList<Plane>();
            adminView.planes = planes;
            return View("adminPanel", adminView);
        }

        public ActionResult SubmitFlight(Flight flight)
        {
            adminView.flight = flight;
            //Check the id of the flight
            bool sameId = (from x in dal.Flights where x.Id == flight.Id select x).Any();
            if (ModelState.IsValid && !sameId)
            {
                Plane plane = (from x in pdal.Planes where x.PlaneId == flight.PlaneId select x).FirstOrDefault();
                flight.Seats = plane.NumSeats;

                dal.Flights.Add(flight);
                dal.SaveChanges();
                adminView.flights = dal.Flights.ToList<Flight>();

                adminView.admin = new Admin();
                adminView.admin = (Admin)Session["Admin"];
                ViewBag.AddedFlight = "You have added a flight succefully!";

                List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
                adminView.flights = flights;

                List<Plane> planes = (from x in pdal.Planes select x).ToList<Plane>();
                adminView.planes = planes;

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

                List<Plane> planes = (from x in pdal.Planes select x).ToList<Plane>();
                adminView.planes = planes;
                return View("adminPanel", adminView);
            }
            return View("EditFlight/" + flight.Id);
        }

        public ActionResult AddPlane()
        {
            adminView.plane = new Plane();
            adminView.planes = new List<Plane>();

            return View(adminView);
        }

        public ActionResult EditPlane(int Id)
        {
            adminView.plane = new Plane();
            adminView.planes = new List<Plane>();
            Plane temp = (from x in pdal.Planes where x.PlaneId == Id select x).FirstOrDefault();
            adminView.plane = temp;
            Session["PlaneId"] = Id;

            return View(adminView);
        }

        public ActionResult SubmitPlane(Plane plane)
        {
            adminView.plane = plane;
            //Check the id of the flight
            bool sameId = (from x in pdal.Planes where x.PlaneId == plane.PlaneId select x).Any();
            if (ModelState.IsValid && !sameId)
            {
                pdal.Planes.Add(plane);
                pdal.SaveChanges();
                adminView.planes = pdal.Planes.ToList<Plane>();

                adminView.admin = new Admin();
                adminView.admin = (Admin)Session["Admin"];
                ViewBag.AddedPlane = "You have added a plane succefully!";

                List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
                adminView.flights = flights;

                List<Plane> planes = (from x in pdal.Planes select x).ToList<Plane>();
                adminView.planes = planes;
                return View("adminPanel", adminView);
            }
            if (sameId)
                ViewBag.SameIdPlane = "The plane Id is already exists!";
            return View("AddFlight");
        }

        public ActionResult SubmitPlaneEdition(Plane plane)
        {
            if (ModelState.IsValid)
            {
                plane.PlaneId = (int)Session["PlaneId"];
                pdal.Planes.AddOrUpdate(plane);
                pdal.SaveChanges();
                adminView.plane = plane;

                adminView.admin = new Admin();
                adminView.admin = (Admin)Session["Admin"];
                ViewBag.AddedPlane = "You have changed a Plane succefully!";

                List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
                adminView.flights = flights;

                List<Plane> planes = (from x in pdal.Planes select x).ToList<Plane>();
                adminView.planes = planes;
                return View("adminPanel", adminView);
            }
            return View("EditPlane/" + plane.PlaneId);
        }
    }
}