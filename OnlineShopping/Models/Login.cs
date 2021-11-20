using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public class Login
    {
        public int id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z']{1,50}", ErrorMessage = "Please enter valid Name")]
        [DisplayName("Name")]

        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }

    }

}