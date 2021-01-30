using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class User
    {
        public int ID { get; set; }

        [Display(Name = "First & Last Name")]
        [Required(ErrorMessage = "Please enter your first and last name.")]
        public string Name { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Username:")]
        [Required(ErrorMessage = "Please a valid username.")]
        public string Username { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a valid password")]
        public string Password { get; set; }

    }
}
