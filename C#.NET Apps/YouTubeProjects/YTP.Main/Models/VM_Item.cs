using System;
using System.Web;

namespace YTP.Main.Models {
    public class VM_Item {
        public Guid ID { get; set; }

        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal ItemPrice { get; set; }

        public HttpPostedFileBase ImagePath { get; set; }

    }
}