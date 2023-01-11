using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApp.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Id has to be more than 1 digit")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Incorrect Input")]
        public int Id { get; set; }
        [Required(ErrorMessage = "From has to be more than 1 character")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Incorrect Input")]
        public string FromCountry { get; set; }
        [Required(ErrorMessage = "To has to be more than 1 character")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Incorrect Input")]
        public string ToCountry { get; set; }
        [Required(ErrorMessage = "Input a Date please!")]
        public DateTime Date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Price has to be more than 1 digit")]
        [RegularExpression("^[+-]?([0-9]*[.])?[0-9]+$", ErrorMessage = "Incorrect Input")]
        public double Price { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Plane Id has to be at least 1 digit")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Incorrect Input")]
        public int PlaneId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Seats { get; set; }
    }
}