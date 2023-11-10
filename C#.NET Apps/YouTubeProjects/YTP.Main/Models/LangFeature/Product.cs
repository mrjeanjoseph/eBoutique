using System.Collections.Generic;

namespace YTP.Main.Models {
    public class Product {

        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ProductPrice { get; set; }
        public string Category { get; set; }


        //private string name { get; set; }   

        //public string Name {
        //    get { return name; }
        //    set {  name = value; }
        //}
    }

    public class ShoppingCart {
        public List<Product> Products { get; set; }
    }

    public static class MyExtentionMethod {
        public static decimal TotalPrices(this ShoppingCart cart) {
            decimal total = 0;
            foreach (var product in cart.Products) {
                total += product.ProductPrice;
            }
            return total;
        }
    }
}