using System.ComponentModel.DataAnnotations;

namespace YTP.Main.Models {
    public class StudentModel {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "PrimaryAddress is required.")]
        public string PrimaryAddress { get; set; }
        public string CityStateZip { get; set; }
        public string PrimaryEmailAddress { get; set; }
        public string PhoneNumber { get; set; }

    }
}