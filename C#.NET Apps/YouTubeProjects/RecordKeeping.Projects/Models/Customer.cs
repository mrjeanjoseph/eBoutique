using System;
using System.ComponentModel.DataAnnotations;

namespace RecordKeeping.Projects.Models {
    public class Customer {
        [Key] public int CustomerID { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter Mobileno")]
        public string Mobileno { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Enter Birthdate")]

        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Enter EmailID")]
        public string EmailID { get; set; }
    }
}