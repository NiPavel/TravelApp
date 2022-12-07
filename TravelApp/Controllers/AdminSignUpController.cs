using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelApp.Models;
using TravelApp.Dal;
using TravelApp.ViewModel;

namespace TravelApp.Controllers
{
    public class AdminSignUpController : Controller
    {
        AdminDal dal = new AdminDal();
        AdminView adminView = new AdminView();

        public ActionResult newAdmin()
        {
            adminView.admin = new SignUp();
            adminView.admins = new List<SignUp>();
            return View(adminView);
        }

        [HttpPost]
        public ActionResult submitAdmin(SignUp admin)
        {
            adminView.admin = admin;
            if(ModelState.IsValid)
            {
                dal.Admins.Add(admin);
                dal.SaveChanges();
                adminView.admins = dal.Admins.ToList<SignUp>();
                return View("adminPanel", adminView);
            }
            return View("newAdmin", adminView);
        }
    }
}