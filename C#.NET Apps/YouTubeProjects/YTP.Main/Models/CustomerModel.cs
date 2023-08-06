using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTP.Main.Models {
    public class CustomerModel {
        public int CustomerId { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string Country { get; set; }
    }
}