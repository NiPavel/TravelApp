﻿using System;
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
                name: "returnToAdminPanel",
                url: "Back/AdminPanel",
                defaults: new { controller = "Home", action = "returnToAdminPanel", id = UrlParameter.Optional }
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

            routes.MapRoute(
                name: "AddFlight",
                url: "AddFlight",
                defaults: new { controller = "AdminPanel", action = "AddFlight", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EditFlight",
                url: "EditFlight/{id}",
                defaults: new { controller = "AdminPanel", action = "EditFlight", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SubmitFlight",
                url: "SubmitFlight",
                defaults: new { controller = "AdminPanel", action = "SubmitFlight", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SubmitEdition",
                url: "SubmitEdition",
                defaults: new { controller = "AdminPanel", action = "SubmitEdition", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
