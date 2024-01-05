using System.ComponentModel.DataAnnotations;

namespace YTP.Main.Areas.HumanResources.Models {
    public class AccEmpData {

        public int EmployeeId { get; set; }

        [Display(Name = "First Name: "), Required(ErrorMessage = "First Name field is required. ")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: "), Required(ErrorMessage = "Last Name field is required. ")]
        public string LastName { get; set; }

        [Display(Name = "Department: "), Required(ErrorMessage = "Department field is required. ")]
        public string Department { get; set; }

        [Display(Name = "Position Type: "), Required(ErrorMessage = "Position Type is required. ")]
        public string PositionType { get; set; }

        [Display(Name = "Salary: "), Required(ErrorMessage = "Salary is required. ")]
        public decimal Salary { get; set; }

        public int CityId { get; set; }

    }
}