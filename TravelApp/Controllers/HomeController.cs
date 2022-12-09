using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult HomePage(string flyOption)
        {
            if(flyOption != null)
                ViewBag.flyOption = flyOption.ToString();
            return View();
        }
    }
}