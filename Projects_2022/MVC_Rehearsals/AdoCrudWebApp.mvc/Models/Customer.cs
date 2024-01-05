using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdoCrudWebApp.mvc.Models {
    public class Customer {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CustomerID { get; set; }

        [Required(ErrorMessage ="Enter your Name: ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your Address: ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter your Mobile Number: ")]
        public string Mobileno { get; set; }

        [Required(ErrorMessage = "Enter your Date of Birth: ")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Enter your Email Address: ")]
        public string EmailID { get; set; }

        public List<Customer> ShowallCustomer { get; set; }
    }
}