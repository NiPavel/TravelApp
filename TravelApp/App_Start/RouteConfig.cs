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
                name: "AuthAdmin",
                url: "AuthAdmin",
                defaults: new { controller = "Home", action = "AuthAdmin", id = UrlParameter.Optional }
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
                name: "addTwoWayFlight",
                url: "addTwoWayFlight/{id}/{second}",
                defaults: new { controller = "Home", action = "addTwoWayFlight", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "makePayment",
                url: "makePayment/{id}",
                defaults: new { controller = "Home", action = "makePayment", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "makePaymentTwo",
                url: "makePaymentTwo/{id}/{second}",
                defaults: new { controller = "Home", action = "makePaymentTwo", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "buyFlight",
                url: "buyFlight/{id}",
                defaults: new { controller = "Home", action = "buyFlight", id = UrlParameter.Optional }
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
                url: "{controller}/SignIn",
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
                name: "AddPlane",
                url: "AddPlane",
                defaults: new { controller = "AdminPanel", action = "AddPlane", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EditFlight",
                url: "EditFlight/{id}",
                defaults: new { controller = "AdminPanel", action = "EditFlight", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EditPlane",
                url: "EditPlane/{id}",
                defaults: new { controller = "AdminPanel", action = "EditPlane", id = UrlParameter.Optional }
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
                name: "SubmitPlane",
                url: "SubmitPlane",
                defaults: new { controller = "AdminPanel", action = "SubmitPlane", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SubmitEdition",
                url: "SubmitEdition",
                defaults: new { controller = "AdminPanel", action = "SubmitEdition", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SubmitPlaneEdition",
                url: "SubmitPlaneEdition",
                defaults: new { controller = "AdminPanel", action = "SubmitPlaneEdition", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "newUserPassword",
                url: "newUserPassword",
                defaults: new { controller = "ChangePass", action = "newUserPassword", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "confirmUserEmail",
                url: "confirmUserEmail",
                defaults: new { controller = "ChangePass", action = "confirmUserEmail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "confirmSecret",
                url: "confirmSecret",
                defaults: new { controller = "ChangePass", action = "confirmSecret", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "confirmPassword",
                url: "confirmPassword",
                defaults: new { controller = "ChangePass", action = "confirmPassword", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
