
using System.ComponentModel.DataAnnotations;

namespace YTP.Main.Areas.HumanResources.Models {
    public class Employee {

        [Display(Name = "Id"), Required(ErrorMessage = "First name is required.")]
        public int Sr_no { get; set; } = 0;

        [Display(Name = "Full Name"), Required(ErrorMessage = "Full Name is required.")]
        public string Emp_name { get; set; } = "";

        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Country { get; set; } = "";
        public string Department { get; set; } = "";
        public string flag { get; set; }
    }
}