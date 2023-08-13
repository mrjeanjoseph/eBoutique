using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTP.AddToCart.ViewModels {
    public class VMViewCart {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public int Qty { get; set; }
        public int UnitPrice { get; set; }
        public string TotalPrice { get; set; }
    }
}