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
        UserDal udal = new UserDal();
        UserView userView = new UserView();
        AdminView adminView = new AdminView();
        
        // GET: Home
        public ActionResult HomePage()
        {
            userView.flight = new Flight();
            userView.user = new User();
            userView.users = new List<User>();
            userView.flights = new List<Flight>();
            //One-way, Two-way options
            string flyOption = Request.Form["flyOption"];
            if(flyOption != null)
                ViewBag.flyOption = flyOption.ToString();

            return View(userView);
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
            return View("HomePage", userView);
        }

        public ActionResult UserSignIn(string email, string password)
        {

            userView.user = new User();
            List<User> users = (from x in udal.Users where (x.Email == email && x.Password == password) select x).ToList<User>();
            if (users.Count != 0)
            {
                Session["UserIn"] = true;
                userView.user = users[0];
                Session["User"] = userView.user;
                return View("MyFlights", userView);
            }
            return View("HomePage", userView);
        }

        public ActionResult SignOut()
        {
            Session["AdminIn"] = null;
            Session["UserIn"] = null;
            return View("HomePage", userView);
        }

        public ActionResult returnToAdminPanel()
        {
            adminView.admin = new Admin();
            adminView.admin = (Admin)Session["Admin"];

            List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
            adminView.flights = flights;
            return View("adminPanel", adminView);
        }

        public ActionResult OneDirectionFly(string fromC, string toC, DateTime date)
        {
            List<Flight> flights = (from x in dal.Flights where (date == x.Date || fromC == x.FromCountry || toC == x.ToCountry) select x).ToList<Flight>();
            userView.flights = flights;
            Session["UserFlights"] = userView.flights;
            return View("HomePage", userView);
        }

        public ActionResult addUserFlight(int Id) { 
            userView.flights = new List<Flight>();
            userView.addedFlights = new List<Flight>();
            Flight temp = (from x in dal.Flights where x.Id == Id select x).FirstOrDefault();
            userView.addedFlights.Add(temp);
            userView.flights = (List<Flight>)Session["UserFlights"];

            return View("HomePage", userView);
        }
    }
}