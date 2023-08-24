using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YTP.Main.ViewModel {
    public class AccEmp {

        public int EmployeeId { get; set; }

        [Display(Name = "First Name: "), Required(ErrorMessage ="FirstName is required. ")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: "), Required(ErrorMessage ="FirstName is required. ")]
        public string LastName { get; set; }

        [Display(Name = "Department: "), Required(ErrorMessage = "Department is required. ")]
        public string Department { get; set; }

        [Display(Name = "Position Type: "), Required(ErrorMessage = "Position Type is required. ")]
        public string PositionType { get; set; }

        [Display(Name = "Salary: "), Required(ErrorMessage = "Salary is required. ")]
        public decimal Salary { get; set; }

        public int CityId { get; set; }

    }
}