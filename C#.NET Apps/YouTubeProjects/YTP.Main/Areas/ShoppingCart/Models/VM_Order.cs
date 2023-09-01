using System;

namespace YTP.Main.Areas.ShoppingCart.Models {
    public class VM_Order {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string Status { get; set; }
    }
} 