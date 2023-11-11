using System;
using System.Collections;
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

    //Applying Extension Methods to an Interface
    public class ShoppingCart2 : IEnumerable<Product> {
        public List<Product> ProductEnum { get; set; }

        public IEnumerator<Product> GetEnumerator() {
            return ProductEnum.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    public static class MyExtentionMethods {
        public static decimal TotalPrices(this IEnumerable<Product> productEnum) {
            decimal total = 0;
            foreach (Product product in productEnum) {
                total += product.ProductPrice;
            }
            return total;
        }

        public static IEnumerable<Product> FilterByCategory(this IEnumerable<Product> productEnum, string categoryParam) {
            foreach (Product product in productEnum) {
                if(product.Category == categoryParam) {
                    yield return product;
                }
            }
        }

        public static IEnumerable<Product> FilterUsingFunc(this IEnumerable<Product> productEnum, Func<Product, bool> selectorParam) {
            foreach (Product product in productEnum) {
                if(selectorParam(product)) {
                    yield return product;
                }
            }
        }
    }



}