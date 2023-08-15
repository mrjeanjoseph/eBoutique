using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YTP.Main.Models {
    public class NorthwindEmployees {

        public int EmployeeID { get; set; }

        public string Title { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }
    }
}