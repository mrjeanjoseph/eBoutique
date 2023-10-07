using System.ComponentModel.DataAnnotations;

namespace YTP.Main.Areas.HumanResources.Models {
    public class NorthwindEmployees {

        public int EmployeeID { get; set; }

        public string Title { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Hire Date")]
        public string HireDate { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }
    }
}