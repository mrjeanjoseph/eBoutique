using System;
using System.Collections.Generic;

namespace YTP.Main.Models {
    public class Item {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public IList<Bid> Bids { get; set; }

        public void AddBid(Member memberParam, decimal amtParam) {
            throw new NotImplementedException();
        }
    }


}