using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelApp.Dal;
using TravelApp.Models;
using TravelApp.ViewModel;

namespace TravelApp.Controllers
{
    public class HomeController : Controller
    {
        AdminDal adminDal = new AdminDal();
        FlightsDal dal = new FlightsDal();
        AdminView adminView = new AdminView();
        // GET: Home
        public ActionResult HomePage()
        {
            //One-way, Two-way options
            string flyOption = Request.Form["flyOption"];
            if(flyOption != null)
                ViewBag.flyOption = flyOption.ToString();

            return View();
        }

        public ActionResult SignIn(string email, string password)
        {
            
            adminView.admin = new Admin();
            List<Admin> admins = (from x in adminDal.Admins where (x.Email == email && x.Password == password) select x).ToList<Admin>();
            if (admins.Count != 0)
            {
                Session["AdminIn"] = true;
                adminView.admin = admins[0];
                Session["Admin"] = adminView.admin;

                List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
                adminView.flights = flights;
                return View("adminPanel", adminView);
            }
            return View("HomePage");
        }

        public ActionResult SignOut()
        {
            Session["AdminIn"] = null;
            return View("HomePage");
        }

        public ActionResult returnToAdminPanel()
        {
            adminView.admin = new Admin();
            adminView.admin = (Admin)Session["Admin"];

            List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
            adminView.flights = flights;
            return View("adminPanel", adminView);
        }
    }
}