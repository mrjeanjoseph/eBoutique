using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YTP.Main.Models {
    public class CustomerModel {

        [Key] public int CustomerID { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Mobileno")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Mobileno")]
        public string Mobileno { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enter Birthdate")]

        public DateTime Birthdate { get; set; }

        public string EmailID { get; set; }

        public List<CustomerModel> ShowallCustomer { get; set; }
    }
}