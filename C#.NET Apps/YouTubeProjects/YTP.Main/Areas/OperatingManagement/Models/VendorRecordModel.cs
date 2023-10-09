using System;
using System.ComponentModel.DataAnnotations;

namespace YTP.Main.Areas.OperatingManagement.Models {
    public class VendorRecordModel {
        public int VendorId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string VendorName { get; set; }
        public string Location { get; set; }
        public Nullable<int> Priority { get; set; }
        public string WebsiteAddress { get; set; }
        public string MainContact { get; set; }
    }
}