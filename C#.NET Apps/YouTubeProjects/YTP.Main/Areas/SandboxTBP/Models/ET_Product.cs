using System.Collections.Generic;
using System.Linq;

namespace YTP.Main.Models {
    public class ET_Product {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string Category { get; set; }
    }


    public class LinqValueCalculator {
        public decimal ValueProducts(IEnumerable<ET_Product> products) {
            return products.Sum(p => p.UnitPrice);
        }
    }

    public class ET_ShoppingCart {

        private LinqValueCalculator calc;
        public ET_ShoppingCart(LinqValueCalculator calcParam) {
            this.calc = calcParam;
        }

        public IEnumerable<ET_Product> Products { get; set; }

        public decimal CalculateProductTotal() {
            return calc.ValueProducts(Products);
        }
    }
}