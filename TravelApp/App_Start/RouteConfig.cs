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
                name: "OneDirection",
                url: "OneDirectionFly",
                defaults: new { controller = "Home", action = "OneDirectionFly", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TwoDirection",
                url: "TwoDirectionFly",
                defaults: new { controller = "Home", action = "TwoDirectionFly", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "addOneWayFlight",
                url: "addOneWayFlight/{id}",
                defaults: new { controller = "Home", action = "addOneWayFlight", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "makePayment",
                url: "makePayment/{id}",
                defaults: new { controller = "Home", action = "makePayment", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "addUserFlight",
                url: "addUserFlight/{id}",
                defaults: new { controller = "Home", action = "addUserFlight", id = UrlParameter.Optional }
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
               name: "newUser",
               url: "newuser",
               defaults: new { controller = "UserSignUp", action = "newUser", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "submitUser",
               url: "submitUser",
               defaults: new { controller = "UserSignUp", action = "submitUser", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "signInUser",
               url: "{controller}/MyFlights",
               defaults: new { controller = "Home", action = "UserSignIn", id = UrlParameter.Optional }
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
                name: "DeleteFlight",
                url: "DeleteFlight/{id}",
                defaults: new { controller = "AdminPanel", action = "DeleteFlight", id = UrlParameter.Optional }
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
