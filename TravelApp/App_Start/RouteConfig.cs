using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TravelApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "HomePage",
                url: "",
                defaults: new {controller = "Home", action = "HomePage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Adminpanel",
                url: "AdminPanel",
                defaults: new { controller = "AdminSignUp", action = "submitAdmin", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "newAdmin",
                url: "newAdmin",
                defaults: new { controller = "AdminSignUp", action = "newAdmin", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SignIn",
                url: "{controller}/AdminPanel",
                defaults: new { controller = "Home", action = "SignIn", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
