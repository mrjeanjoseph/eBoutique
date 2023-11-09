using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YTP.Main.Models {
    public class GuestResponse {
        [Required(ErrorMessage = "Please enter your name")]
        public string RSVP_Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter your Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please specify whether you will attend")]
        public bool? WillAttend { get; set; }

    }
}