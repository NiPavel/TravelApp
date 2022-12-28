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
    public class UserSignUpController : Controller
    {
        UserDal dal = new UserDal();
        UserView userView= new UserView();
        // GET: UserSignUp
        public ActionResult newUser()
        {
            userView.user = new User();
            userView.users = new List<User>();
            return View(userView);
        }

        public ActionResult submitUser(User user)
        {
            userView.user = user;
            if (ModelState.IsValid)
            {
                dal.Users.Add(user);
                dal.SaveChanges();
                userView.users = dal.Users.ToList<User>();
                Session["UserIn"] = true;
                Session["User"] = userView.user;
                return View("MyFlights", userView);
            }
            return View("newUser", userView);
        }
    }
}