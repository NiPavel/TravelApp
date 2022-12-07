using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelApp.Models
{
    public class SignUp
    {

        [Required(ErrorMessage = "Username has to be more than 2 characters")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username has to be more than 2 charcatres")]
        public string Username { get; set; }
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

    }
}