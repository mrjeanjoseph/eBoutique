using System.Collections.Generic;
using YTP.Domain.SportsStore.Entities;

namespace YTP.Main.Areas.SportsStore.Models {
    public class ProductsList_VM {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}