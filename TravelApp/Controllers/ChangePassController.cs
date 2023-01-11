using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelApp.Dal;
using TravelApp.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using System.Data.Entity.Migrations;

namespace TravelApp.Controllers
{
    public class ChangePassController : Controller
    {
        UserDal udal = new UserDal();
        AdminDal adal = new AdminDal();

        // GET: ChangePass
        public ActionResult newUserPassword()
        {
            return View();
        }

        public ActionResult confirmUserEmail(string email) {

            User user = (from x in udal.Users where email == x.Email select x).FirstOrDefault();
            Admin admin = (from x in adal.Admins where email == x.Email select x).FirstOrDefault();
            if (user != null || admin != null)
            {
                Random generator = new Random();
                String secretCode = generator.Next(0, 1000000).ToString("D6");
                Session["Secret"] = secretCode;
                Session["emailUser"] = user;
                Session["emailAdmin"] = admin;

                string fromMail = "travelbuy4@gmail.com";
                string fromPassword = "ookzbtkhxhekyqml";

                MailMessage message= new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "Travel Buy Password Reset";
                message.To.Add(new MailAddress(email));
                message.Body = "<html><body> The secret code:" + secretCode + "</body></html>";
                message.IsBodyHtml= true;

                var smptClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl= true
                };

                smptClient.Send(message);
                Session["HideEmail"] = true;
                ViewBag.rightEmail = "We've send an email with secret code to your mail.";
            }
            else
            {
                ViewBag.wrongEmail = "This email doesnt exists! Try one more time please.";
            }

            return View("newUserPassword");
        }

        public ActionResult confirmSecret(string secret) { 
            
            if(secret == (string)Session["Secret"])
            {
                ViewBag.RightSecret = "Eneter a new password please";
                Session["Secret"] = null;
            }
            else
            {
                ViewBag.WrongSecret = "You have entered a wrong code! Try again Please.";
            }


            return View("NewUserPassword");
        }

        public ActionResult confirmPassword(string password)
        {
            if (password.Length >= 6)
            {
                if ((User)Session["emailUser"] != null)
                {
                    User user = (User)Session["emailUser"];
                    user.Password = password;
                    udal.Users.AddOrUpdate(user);
                    udal.SaveChanges();
                }

                if ((Admin)Session["emailAdmin"] != null)
                {
                    Admin admin = (Admin)Session["emailAdmin"];
                    admin.Password = password;
                    adal.Admins.AddOrUpdate(admin);
                    adal.SaveChanges();
                }

                ViewBag.ChangedPassword = "You have changed your password succefully!";
                Session["HideEmail"] = null;
                Session["emailUser"] = null;
                Session["emailAdmin"] = null;
            }
            else
            {
                ViewBag.RightSecret = "Eneter a new password please";
                ViewBag.passLength = "Password has to be more than 6 characters.";
            }
            return View("NewUserPassword");

        }
    }
}