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
    public class HomeController : Controller
    {
        AdminDal adminDal = new AdminDal();
        FlightsDal dal = new FlightsDal();
        UserDal udal = new UserDal();
        OrderDal odal = new OrderDal();


        UserView userView = new UserView();
        AdminView adminView = new AdminView();
        
        // GET: Home
        public ActionResult HomePage()
        {
            userView.flight = new Flight();
            userView.user = new User();
            userView.users = new List<User>();
            userView.flights = new List<Flight>();
            userView.order = new Order();
            userView.orders = new List<Order>();

            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            //One-way, Two-way options
            string flyOption = Request.Form["flyOption"];
            if(flyOption != null)
                ViewBag.flyOption = flyOption.ToString();

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            return View(userView);
        }

        public ActionResult MyFlightsPage()
        {
            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            return View("MyFlights", userView);
        }

        public ActionResult SignIn(string email, string password)
        {
            userView.flights = new List<Flight>();
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

            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            return View("HomePage", userView);
        }

        public ActionResult UserSignIn(string email, string password)
        {
            userView.flights = new List<Flight>();
            userView.user = new User();
            List<User> users = (from x in udal.Users where (x.Email == email && x.Password == password) select x).ToList<User>();
            if (users.Count != 0)
            {
                Session["UserIn"] = true;
                userView.user = users[0];
                Session["User"] = userView.user;
                Session["IdCount"] = 1;
                Session["pickedFlight"] = 99999;
                return View("MyFlights", userView);
            }

            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            return View("HomePage", userView);
        }

        public ActionResult SignOut()
        {
            userView.flights = new List<Flight>();
            Session["AdminIn"] = null;
            Session["UserIn"] = null;
            Session["IdCount"] = 1;
            Session["User"] = null;
            Session["choosenFlights"] = null;
            Session["pickedFlight"] = null;

            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            return View("HomePage", userView);
        }

        public ActionResult returnToAdminPanel()
        {
            adminView.admin = new Admin();
            adminView.admin = (Admin)Session["Admin"];

            List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
            adminView.flights = flights;

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            return View("adminPanel", adminView);
        }

        public ActionResult OneDirectionFly(string fromC, string toC, DateTime date)
        {
            userView.oneWay = new List<Flight>();

            List<Flight> flights = (from x in dal.Flights where (date == x.Date || fromC == x.FromCountry && toC == x.ToCountry) select x).ToList<Flight>();
            userView.oneWay = flights;

            Session["UserFlights"] = userView.oneWay;

            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            return View("HomePage", userView);
        }

        public ActionResult TwoDirectionFly(string fromC, string toC, DateTime sdate, DateTime edate)
        {
            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            List<Flight> toFlight = (from x in dal.Flights where (sdate == x.Date || fromC == x.FromCountry && toC == x.ToCountry) select x).ToList<Flight>();
            List<Flight> returnFlight = (from x in dal.Flights where (sdate == x.Date || fromC == x.ToCountry && toC == x.FromCountry) select x).ToList<Flight>();

            if (sdate <= edate && returnFlight.Count > 0)
            {
                
                toFlight.AddRange(returnFlight);
                userView.twoWay = toFlight;

                Session["UserFlights"] = userView.twoWay;

                return View("HomePage", userView);

            }
            else
            {

                return View("HomePage", userView);
            }
        }

        public ActionResult addOneWayFlight(int Id)
        {
            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            userView.choosenFlights = new List<List<Flight>>();
            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            Flight choosen_flight = (from x in dal.Flights where x.Id == Id select x).FirstOrDefault();

            if ((int)Session["pickedFlight"] != Id || Session["pickedFlight"] == null)
            {
                if (Session["FlightId"] != null)
                {
                    int id = (int)Session["FlightId"];
                    Flight choosen_flight2 = (from x in dal.Flights where x.Id == id select x).FirstOrDefault();
                    List<Flight> temp_flights2 = new List<Flight>();
                    temp_flights2.Add(choosen_flight);
                    temp_flights2.Add(choosen_flight2);
                    userView.choosenFlights.Add(temp_flights2);
                    Session["FlightId"] = null;
                }
                else
                {
                    List<Flight> temp_flight = new List<Flight>();
                    temp_flight.Add(choosen_flight);
                    userView.choosenFlights.Add(temp_flight);
                }
                Session["choosenFlights"] = userView.choosenFlights;
                Session["pickedFlight"] = Id;

            }

            return View("MyFlights", userView);
        }

        public ActionResult buyFlight(int Id) {
            userView.order = new Order();
            userView.user = new User();
            userView.flight= new Flight();

            userView.flights = new List<Flight>();
            userView.choosenFlights = new List<List<Flight>>();
            userView.orders = new List<Order>();

            Flight temp = (from x in dal.Flights where x.Id == Id select x).FirstOrDefault();
            userView.flights = (List<Flight>)Session["UserFlights"];
            userView.user = (User)Session["User"];
            userView.flight = temp;

            List<Order> order_temp = (from x in odal.Orders select x).ToList<Order>();

            if (order_temp.Count == 0)
                userView.order.Id = (int)Session["IdCount"];
            else
            {
                userView.order.Id = order_temp[order_temp.Count - 1].Id + 1;
            }
            userView.order.FName = userView.user.FirstName;
            userView.order.LName = userView.user.LastName;
            userView.order.Email = userView.user.Email;
            userView.order.FlightId = userView.flight.Id;
            userView.order.FromCountry = userView.flight.FromCountry;
            userView.order.ToCountry = userView.flight.ToCountry;
            userView.order.Date = userView.flight.Date;
            userView.order.Price = userView.flight.Price;

            odal.Orders.Add(userView.order);
            odal.SaveChanges();
            userView.orders = odal.Orders.ToList<Order>();

            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            return View("HomePage", userView);
        }
    }
}