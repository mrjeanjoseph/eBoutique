using System;
using System.Collections.Generic;
using System.Linq;

namespace YTP.Main.Models {
    public class Item {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public IList<Bid> Bids { get; set; }

        public Item() {
            Bids = new List<Bid>(); 
        }

        public void AddBid(Member memberParam, decimal amtParam) {
            if (Bids.Count() == 0 || amtParam > Bids.Max(e => e.BidAmount)) {
                Bids.Add(new Bid() {
                    BidAmount = amtParam,
                    DatePlaced = DateTime.Now,
                    Member = memberParam
                });
            } else {
                throw new InvalidOperationException("Bid amount too low"); ;
            }
        }
    }
}