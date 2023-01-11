using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelApp.Models
{
    public class Plane
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Id has to be more than 1 digit")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Incorrect Input")]
        public int PlaneId { get; set; }
        [Required(ErrorMessage = "Plane Type has to be more than 1 character")]
        public string PlaneType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Number of seats has to be more than 1 digit")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Incorrect Input")]
        public int NumSeats { get; set; }
    }
}