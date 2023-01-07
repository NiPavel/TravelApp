using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelApp.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Id has to be more than 1 digit")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Incorrect Input")]
        public int PayId { get; set; }
        [Required(ErrorMessage = "First name has to be more than 2 characters")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name has to be more than 2 charcatres")]
        public string PayFirstName { get; set; }
        [Required(ErrorMessage = "Last name has to be more than 2 characters")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name has to be more than 2 charcatres")]
        public string PayLastName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Card Number has to be 16 digits")]
        public int CardNumber { get; set; }
    }
}