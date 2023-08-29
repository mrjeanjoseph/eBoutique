using System;

namespace YTP.Main.Areas.ShoppingCart.Models {
    public class VM_ShoppingCart {

        public Guid ItemId { get; set; }

        public string ItemName { get; set; }
        public string ItemPath { get; set; }
        public string Description { get; set; }
        public decimal ItemPrice { get; set; }

    }
}