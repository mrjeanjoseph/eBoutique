using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTP.Main.Models {
    public class GuestResponse {
        public string RSVP_Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool WillAttend { get; set; }

    }
}