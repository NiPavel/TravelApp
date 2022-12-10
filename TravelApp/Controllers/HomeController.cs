using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelApp.Dal;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class HomeController : Controller
    {
        AdminDal adminDal = new AdminDal();
        // GET: Home
        public ActionResult HomePage(string flyOption)
        {
            //One-way, Two-way options
            if(flyOption != null)
                ViewBag.flyOption = flyOption.ToString();

            return View();
        }

        public ActionResult SignIn(string email, string password)
        {
            List<SignUp> admins = (from x in adminDal.Admins where (x.Email.Contains(email) && x.Password == password) select x).ToList<SignUp>();
            if (admins.Count != 0)
            {
                return View("adminPanel");
            }
            return View("HomePage");
        }
    }
}