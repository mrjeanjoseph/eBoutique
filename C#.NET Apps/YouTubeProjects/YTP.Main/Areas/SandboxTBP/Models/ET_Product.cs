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

    public interface IValueCalculator {
        decimal ValueProducts(IEnumerable<ET_Product> products);
    }

    public class LinqValueCalculator : IValueCalculator {

        private readonly IDiscountHelper _discounter;

        public LinqValueCalculator(IDiscountHelper discountParam) {
            _discounter = discountParam;
        }

        public decimal ValueProducts(IEnumerable<ET_Product> products) {

            //var sumResult = products.Sum(p => p.UnitPrice);
            var discountResult = _discounter.ApplyDiscount(products.Sum(p => p.UnitPrice));

            return discountResult;
        }
    }

    public class ET_ShoppingCart {

        private readonly IValueCalculator calc;
        public ET_ShoppingCart(IValueCalculator calcParam) {
            this.calc = calcParam;
        }

        public IEnumerable<ET_Product> Products { get; set; }

        public decimal CalculateProductTotal() {
            return calc.ValueProducts(Products);
        }
    }
}