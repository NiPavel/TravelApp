using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelApp.Models
{
    public class Admin
    {

        [Required(ErrorMessage = "First name has to be more than 2 characters")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name has to be more than 2 charcatres")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name has to be more than 2 characters")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name has to be more than 2 charcatres")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username has to be more than 6 characters")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password has to be more than 6 characters")]
        public string Password { get; set; }
        [Key]
        [Required(ErrorMessage = "Enter the mail please!")]
        [RegularExpression("^[a-z0-9]+@[a-z]+\\.[a-z]{2,3}$")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter the phone please!")]
        [RegularExpression("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$")]
        public string Phone { get; set; }
        public string Ip { get; set; }

        public Admin() { }
        public Admin(object[] objects)
        {
            FirstName = objects[0].ToString();
            LastName = objects[1].ToString();
            Password = objects[2].ToString();
            Email = objects[3].ToString();
            Phone = objects[4].ToString();
            Ip = objects[5].ToString();
        }

    }
}