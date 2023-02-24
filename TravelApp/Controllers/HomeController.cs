using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TravelApp.Dal;
using TravelApp.Models;
using TravelApp.ViewModel;
using TwoFactorAuth;

namespace TravelApp.Controllers
{
    public class HomeController : Controller
    {
        TFA tfa = new TFA();

        public static int count = 0;
        AdminDal adminDal = new AdminDal();
        FlightsDal dal = new FlightsDal();
        UserDal udal = new UserDal();
        OrderDal odal = new OrderDal();
        PaymentDal pdal = new PaymentDal();
        PlaneDal pldal = new PlaneDal();


        UserView userView = new UserView();
        AdminView adminView = new AdminView();
        
        // GET: Home
        public ActionResult HomePage()
        {
            count = 0;
            userView.flight = new Flight();
            userView.user = new User();
            userView.users = new List<User>();
            userView.flights = new List<Flight>();
            userView.order = new Order();
            userView.orders = new List<Order>();
            userView.payment = new Payment();
            userView.payments = new List<Payment>();

            Session["HideEmail"] = null;
            Session["emailUser"] = null;
            Session["emailAdmin"] = null;
            Session["Secret"] = null;

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

            userView.boughtFlights = (from x in odal.Orders where userView.user.Email == x.Email select x).ToList<Order>();

            return View("MyFlights", userView);
        }

        public ActionResult AuthAdmin(string secret)
        {
            tfa = (TFA)Session["tfa"];
            string msg = tfa.CheckSecretCode(secret);
            
            if (Session["reSend"] != null)
            {
                 tfa.SendCode();
            }
            

            if (msg == "valid")
            {
                Session["reSend"] = null;
                if (Session["Admin"] != null)
                {
                    Session["AdminIn"] = true;
                    adminView.admin = (Admin)Session["Admin"];
                    if (tfa.getChangedIp())
                    {
                        adminView.admin.Ip = tfa.getIpAdress();
                        adminDal.Admins.AddOrUpdate(adminView.admin);
                        adminDal.SaveChanges();
                    }
                    List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
                    adminView.flights = flights;
                    List<Plane> planes = (from x in pldal.Planes select x).ToList<Plane>();
                    adminView.planes = planes;
                    return View("adminPanel", adminView);
                }
                else
                {
                    Session["UserIn"] = true;
                    userView.user = (User)Session["User"];
                    if (tfa.getChangedIp())
                    {
                        userView.user.Ip = tfa.getIpAdress();
                        udal.Users.AddOrUpdate(userView.user);
                        udal.SaveChanges();
                    }
                    Session["IdCount"] = 1;
                    Session["pickedFlight"] = 99999;
                    userView.boughtFlights = (from x in odal.Orders where userView.user.Email == x.Email select x).ToList<Order>();
                    return View("MyFlights", userView);
                }
            }
            else if (msg == "wrong")
            {
                Session["reSend"] = null;
                ViewBag.wrongSecret = "The secret code is wrong!";
                return View("Auth");
            }
            else
            {
                Session["reSend"] = true;
                ViewBag.wrongSecret = "Timeout!";
                return View("Auth");
            }
        }

        public ActionResult SignIn(string email, string password, bool sendSms=false, bool sendEmail=false)
        {
            string stringConnection = "Data Source=DESKTOP-MICG2LQ; Integrated Security=true;Initial Catalog=TravelProject;";
            string query = "SELECT * From dbo.Admins";
            string dbPassword = "Password";
            string dbUsername = "Email";


            Session["HideEmail"] = null;
            Session["emailUser"] = null;
            Session["emailAdmin"] = null;
            Session["Secret"] = null;

            tfa.sendEmail = sendEmail;
            tfa.sendSMS = sendSms;

            userView.flights = new List<Flight>();

            Admin admin = new Admin(tfa.authenticate(stringConnection, query, dbUsername, dbPassword, email, password));
            if (admin != null)
            {
                adminView.admin = admin;
                tfa.setEmailandPhone(admin.Email, admin.Phone);

                Session["codeSent"] = tfa.SendCode();
                Session["tfa"] = tfa;
                if ((bool)Session["codeSent"])
                {
                    Session["Admin"] = adminView.admin;
                    return View("Auth", adminView);
                }
            }
            if (!(bool)Session["codeSent"])
                ViewBag.noUser = "Email or password is incorrect!";

            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            return View("HomePage", userView);
        }

        public ActionResult UserSignIn(string email, string password, bool sendSms = false, bool sendEmail = false)
        {
            string stringConnection = "Data Source=DESKTOP-MICG2LQ; Integrated Security=true;Initial Catalog=TravelProject;";
            string query = "SELECT * From dbo.Users";
            string dbPassword = "Password";
            string dbUsername = "Email";

            Session["HideEmail"] = null;
            Session["emailUser"] = null;
            Session["emailAdmin"] = null;
            Session["Secret"] = null;

            tfa.sendEmail = sendEmail;
            tfa.sendSMS = sendSms;

            userView.flights = new List<Flight>();
            userView.user = new User();

            User user = new User(tfa.authenticate(stringConnection, query, dbUsername, dbPassword, email, password));
            if (user != null)
            {
                userView.user = user;
                tfa.setEmailandPhone(user.Email, user.Phone);

                Session["codeSent"] = tfa.SendCode();
                Session["tfa"] = tfa;
                if ((bool)Session["codeSent"])
                {
                    Session["User"] = userView.user;
                    return View("Auth", userView);
                }
            }
            if (!(bool)Session["codeSent"])
                ViewBag.noUser = "Email or password is incorrect!";

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
            List<Flight> flights = (from x in dal.Flights select x).ToList<Flight>();
            adminView.flights = flights;

            List<Plane> planes = (from x in pldal.Planes select x).ToList<Plane>();
            adminView.planes = planes;

            if (Session["Admin"] != null)
                adminView.admin = (Admin)Session["Admin"];

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

            if (count == 0)
            {
                Flight choosen_flight = (from x in dal.Flights where x.Id == Id select x).FirstOrDefault();
                List<Flight> temp_flight = new List<Flight>();
                temp_flight.Add(choosen_flight);
                userView.choosenFlights.Add(temp_flight);
                
                Session["choosenFlights"] = userView.choosenFlights;
                Session["pickedFlight"] = Id;
                count += 1;

            }
            userView.boughtFlights = (from x in odal.Orders where userView.user.Email == x.Email select x).ToList<Order>();

            return View("MyFlights", userView);
        }

        public ActionResult addTwoWayFlight(int Id, int second)
        {
            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            userView.choosenFlights = new List<List<Flight>>();
            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];
            if (count == 0)
            {
                Flight choosen_flight = (from x in dal.Flights where x.Id == Id select x).FirstOrDefault();
                Flight choosen_flight2 = (from x in dal.Flights where x.Id == second select x).FirstOrDefault();
                List<Flight> temp_flights2 = new List<Flight>();
                temp_flights2.Add(choosen_flight);
                temp_flights2.Add(choosen_flight2);
                userView.choosenFlights.Add(temp_flights2);
                Session["choosenFlights"] = userView.choosenFlights;
            }

            userView.boughtFlights = (from x in odal.Orders where userView.user.Email == x.Email select x).ToList<Order>();

            return View("MyFlights", userView);
        }

        public ActionResult makePayment(int Id, int numOfTickets)
        {
            userView.payment = new Payment();
            userView.payments = new List<Payment>();

            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            Session["NumberOfTickets"] = numOfTickets;
            Flight choosen_flight = (from x in dal.Flights where x.Id == Id select x).FirstOrDefault();
            userView.flight = choosen_flight;

            if (choosen_flight.Seats == 0 || choosen_flight.Date < DateTime.UtcNow)
            {
                List<Flight> temp_flights2 = (from x in dal.Flights select x).ToList<Flight>();
                if (temp_flights2.Count != 0)
                    userView.flights = temp_flights2;

                if (Session["choosenFlights"] != null)
                    userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

                userView.boughtFlights = (from x in odal.Orders where userView.user.Email == x.Email select x).ToList<Order>();

                if(choosen_flight.Seats == 0)
                    ViewBag.noFlight = "Sorry, Not enought seats in the flight!";
                else
                    ViewBag.noFlight = "Sorry, the flight is alredy departed!";
                return View("MyFlights", userView);
            }

            List<Flight> temp_flight = new List<Flight>();
            temp_flight.Add(choosen_flight);

            double price = 0;
            foreach (Flight fl in temp_flight)
                price += fl.Price;
            price *= numOfTickets;
            Session["ThePrice"] = price;

            return View("MakePayment", userView);
        }

        public ActionResult makePaymentTwo(int Id, int numOfTickets, int second)
        {
            userView.payment = new Payment();
            userView.payments = new List<Payment>();

            List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
            if (temp_flights.Count != 0)
                userView.flights = temp_flights;

            if (Session["User"] != null)
                userView.user = (User)Session["User"];

            if (Session["choosenFlights"] != null)
                userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

            Session["NumberOfTickets"] = numOfTickets;
            Flight choosen_flight = (from x in dal.Flights where x.Id == Id select x).FirstOrDefault();
            userView.flight = choosen_flight;
            Flight choosen_flight2 = (from x in dal.Flights where x.Id == second select x).FirstOrDefault();
            if (choosen_flight2.Seats == 0 || choosen_flight.Seats == 0 || choosen_flight.Date < DateTime.UtcNow)
            {

                List<Flight> temp_flights2 = (from x in dal.Flights select x).ToList<Flight>();
                if (temp_flights2.Count != 0)
                    userView.flights = temp_flights2;

                if (Session["choosenFlights"] != null)
                    userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

                userView.boughtFlights = (from x in odal.Orders where userView.user.Email == x.Email select x).ToList<Order>();
                if (choosen_flight2.Seats == 0 || choosen_flight.Seats == 0)
                    ViewBag.noFlight = "Sorry, Not enought seats in the flight!";
                else
                    ViewBag.noFlight = "Sorry, the flight is alredy departed!";
                return View("MyFlights", userView);
            }
            List<Flight> temp_flight = new List<Flight>();
            temp_flight.Add(choosen_flight);
            temp_flight.Add(choosen_flight2);

            double price = 0;
            foreach (Flight fl in temp_flight)
                price += fl.Price;
            price *= numOfTickets;
            Session["ThePrice"] = price;
            Session["FlightId"] = second;

            return View("MakePayment", userView);
        }

        [HttpPost]
        public ActionResult buyFlight(Payment payment, int Id, bool savePay=false)
        {
            userView.payment = new Payment();
            userView.payment = payment;
            userView.order = new Order();
            userView.user = new User();
            userView.flight = new Flight();

            userView.flights = new List<Flight>();
            userView.boughtFlights = new List<Order>();
            userView.orders = new List<Order>();
            userView.payments = new List<Payment>();

            userView.user = (User)Session["User"];

            Flight temp = (from x in dal.Flights where x.Id == Id select x).FirstOrDefault();
            userView.flight = temp;

            if (ModelState.IsValid)
            {
                //Check for 2way flights
                List<Flight> temp_flight = new List<Flight>();
                if (Session["FlightId"] != null)
                {
                    int id = (int)Session["FlightId"];
                    Flight choosen_flight2 = (from x in dal.Flights where x.Id == id select x).FirstOrDefault();
                    temp_flight.Add(temp);
                    temp_flight.Add(choosen_flight2);
                    Session["FlightId"] = null;
                }
                else
                    temp_flight.Add(temp);


                for (int i = 0; i < (int)Session["NumberOfTickets"]; i++)
                {
                    foreach (Flight flight in temp_flight)
                    {
                        userView.order = new Order();
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
                        userView.order.FlightId = flight.Id;
                        userView.order.FromCountry = flight.FromCountry;
                        userView.order.ToCountry = flight.ToCountry;
                        userView.order.Date = flight.Date;
                        userView.order.Price = flight.Price;
                        flight.Seats -= 1;
                        dal.Flights.AddOrUpdate(flight);
                        dal.SaveChanges();
                        userView.orders.Add(userView.order);
                        odal.Orders.Add(userView.order);
                        odal.SaveChanges();
                    }
                }

                //Delete bought flight from the list
                if (Session["choosenFlights"] != null)
                    userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];
                foreach (List<Flight> flight in userView.choosenFlights)
                {
                    if (temp_flight.Count == 2 && flight.Count == 2 && flight[0].Id == temp_flight[0].Id && flight[1].Id == temp_flight[1].Id)
                    {
                        flight.RemoveAll(line => (flight[0].Id == temp_flight[0].Id && flight[1].Id == temp_flight[1].Id));
                        break;
                    }
                    if (flight.Count == 1 && flight[0].Id == temp_flight[0].Id)
                    {
                            flight.RemoveAll(line => flight[0].Id == temp_flight[0].Id);
                            break;
                    }
                }
                userView.choosenFlights.Remove(temp_flight);
                Session["choosenFlights"] = userView.choosenFlights;

                if (savePay)
                { 
                    userView.payments = (from x in pdal.Payments select x).ToList<Payment>();
                    userView.payment.Id = userView.payments.Count;
                    userView.payment.Price = (double)Session["ThePrice"];
                    pdal.Payments.Add(userView.payment);
                    pdal.SaveChanges();
                    userView.payments = pdal.Payments.ToList<Payment>();

                    ViewBag.payment = "Thank You for your charge, the payment has saved!";
                }
                else
                    ViewBag.payment = "Thank you for your charge!";

                List<Flight> temp_flights = (from x in dal.Flights select x).ToList<Flight>();
                if (temp_flights.Count != 0)
                    userView.flights = temp_flights;

                if (Session["choosenFlights"] != null)
                    userView.choosenFlights = (List<List<Flight>>)Session["choosenFlights"];

                return View("HomePage", userView);
            }
            return View("MakePayment", userView);
        }

    }
}