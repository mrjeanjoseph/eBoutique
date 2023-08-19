using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YTP.Main.Models {
    public class ProductModel {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Barcode { get; set; }
        public int StockQty { get; set; }    
    }
}