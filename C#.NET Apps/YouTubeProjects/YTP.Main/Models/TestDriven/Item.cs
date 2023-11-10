using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTP.Main.Models {
    public class Item {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public IList<Item> Bids { get; set; }
    }
}