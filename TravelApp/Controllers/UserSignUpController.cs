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
            bool userEmail = (from x in dal.Users where x.Email == user.Email select x).Any();
            if (ModelState.IsValid && !userEmail)
            {
                dal.Users.Add(user);
                dal.SaveChanges();
                userView.users = dal.Users.ToList<User>();
                Session["UserIn"] = true;
                Session["User"] = userView.user;
                return View("MyFlights", userView);
            }
            if (userEmail)
                ViewBag.sameEmail = "The user with same Email is already registred!";
            return View("newUser", userView);
        }
    }
}