using System.ComponentModel.DataAnnotations;

namespace YTP.Domain.SportsStore.Entities {
    public class ShippingDetails {

        [Required(ErrorMessage = "Please enter full name")]
        [Display(Name = "Customer Name")] public string CustomerName { get; set; }


        [Required(ErrorMessage = "Please enter address")]
        [Display(Name = "Line 1")] public string AddressLine1 { get; set; }
        [Display(Name = "Line 2")] public string AddressLine2 { get; set; }
        [Display(Name = "Line 3")] public string AddressLine3 { get; set; }

        [Required(ErrorMessage = "Please enter City")]
        public string City { get; set;}

        [Required(ErrorMessage = "Please enter State")]
        public string State { get; set;}

        public string PostalCode { get; set;}

        [Required(ErrorMessage = "Please enter Country")]
        public string Country { get; set;}
        public string Phone { get; set;}
        public bool GiftWrap { get; set;}

    }
}
