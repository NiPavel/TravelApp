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
            AdminView adminView = new AdminView();
            adminView.admin = new Admin();
            List<Admin> admins = (from x in adminDal.Admins where (x.Email == email && x.Password == password) select x).ToList<Admin>();
            if (admins.Count != 0)
            {
                Session["AdminIn"] = true;
                adminView.admin = admins[0];
                return View("adminPanel", adminView);
            }
            return View("HomePage");
        }

        public ActionResult SignOut()
        {
            Session["AdminIn"] = null;
            return View("HomePage");
        }
    }
}