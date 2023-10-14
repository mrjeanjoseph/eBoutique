using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models {
    public class RentalViewModel {
        public int id { get; set; }
        public string carid { get; set; }
        public Nullable<int> custid { get; set; }
        public Nullable<int> fee { get; set; }
        public Nullable<System.DateTime> sdate { get; set; }
        public Nullable<System.DateTime> edate { get; set; }
    }
}