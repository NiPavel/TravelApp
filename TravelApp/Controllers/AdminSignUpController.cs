using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelApp.Models;
using TravelApp.Dal;
using TravelApp.ViewModel;
using System.Web.UI.WebControls;
using TwoFactorAuth;

namespace TravelApp.Controllers
{
    public class AdminSignUpController : Controller
    {
        TFA tfa = new TFA();

        private static string secret = "Secret";
        AdminDal dal = new AdminDal();
        UserDal udal = new UserDal();
        FlightsDal fdal = new FlightsDal();
        PlaneDal pdal = new PlaneDal();
        AdminView adminView = new AdminView();


        public ActionResult newAdmin()
        {
            adminView.admin = new Admin();
            adminView.admins = new List<Admin>();
            return View(adminView);
        }

        [HttpPost]
        public ActionResult submitAdmin(Admin admin, string secretWord)
        {
            adminView.admin = admin;
            bool sameEmail = (from x in dal.Admins where x.Email == admin.Email select x).Any();
            if (secretWord == secret)
            {
                if (ModelState.IsValid && !sameEmail)
                {
                    adminView.admin.Ip = tfa.getIpAdress();
                    adminView.admin.Password = tfa.encrypt(admin.Password);
                    dal.Admins.Add(adminView.admin);
                    dal.SaveChanges();

                    User user = new User();
                    user.FirstName = admin.FirstName;
                    user.LastName = admin.LastName;
                    user.Email = admin.Email;
                    user.Password = adminView.admin.Password;
                    user.Phone= admin.Phone;
                    user.Ip = adminView.admin.Ip;
                    udal.Users.Add(user);
                    udal.SaveChanges();

                    adminView.admins = dal.Admins.ToList<Admin>();
                    adminView.flights = fdal.Flights.ToList<Flight>();
                    adminView.planes = pdal.Planes.ToList<Plane>();
                    Session["Admin"] = admin;
                    Session["AdminIn"] = true;
                    return View("adminPanel", adminView);
                }
            }
            else { 
                ViewBag.secret = "The wrong Secret Word!"; 
            }
            if (sameEmail)
                ViewBag.sameEmail = "The admin with same Email is already registred!";
            return View("newAdmin", adminView);
        }
    }
}