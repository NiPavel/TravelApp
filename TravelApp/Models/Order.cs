using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelApp.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Id has to be more than 1 digit")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Incorrect Input")]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Id has to be more than 1 digit")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Incorrect Input")]
        public int FlightId { get; set; }
        [Required(ErrorMessage = "First name has to be more than 2 characters")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name has to be more than 2 charcatres")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Last name has to be more than 2 characters")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name has to be more than 2 charcatres")]
        public string LName { get; set; }
        [Required(ErrorMessage = "From has to be more than 1 character")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Incorrect Input")]
        public string FromCountry { get; set; }
        [Required(ErrorMessage = "To has to be more than 1 character")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Incorrect Input")]
        public string ToCountry { get; set; }
        [Required(ErrorMessage = "Enter the mail please!")]
        [RegularExpression("^[a-z0-9]+@[a-z]+\\.[a-z]{2,3}$")]
        public string Email { get; set; }
        [Required(ErrorMessage = "To has to be more than 1 character")]
        public DateTime Date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Price has to be more than 1 digit")]
        [RegularExpression("^[+-]?([0-9]*[.])?[0-9]+$", ErrorMessage = "Incorrect Input")]
        public double Price { get; set; }


    }
}