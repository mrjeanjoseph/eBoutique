using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YTP.Main.Areas.VehicleRentalSystem.Models {

    [MetadataType(typeof(CustomerMetaData))]

    public partial class Customer {

        public class CustomerMetaData {

            [DisplayName("Customer Name")] public string CustomerName { get; set; }
            [DisplayName("Address")] public string CompleteAddress { get; set; }
            [DisplayName("Phone Number")] public int? PhoneNumber { get; set; }
        }

    }
}