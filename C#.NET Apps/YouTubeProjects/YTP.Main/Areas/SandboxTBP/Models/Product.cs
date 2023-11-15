using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace YTP.Main.Models {
    public class Product {

        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ProductPrice { get; set; }
        public string Category { get; set; }
    }

    #region Ch1-5
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
                if (product.Category == categoryParam) {
                    yield return product;
                }
            }
        }

        public static IEnumerable<Product> FilterUsingFunc(this IEnumerable<Product> productEnum, Func<Product, bool> selectorParam) {
            foreach (Product product in productEnum) {
                if (selectorParam(product)) {
                    yield return product;
                }
            }
        }
    }

    public class MyAsyncMethods {
        public static Task<long?> GetPageLength() {
            HttpClient client = new HttpClient();
            var httpTask = client.GetAsync("http://apress.com");

            //Other logic to address while we're waiting for http request to complete.

            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) => {
                return antecedent.Result.Content.Headers.ContentLength;
            });
        }
    }

    public class MyAsyncAwaitMethods {
        public async static Task<long?> GetPageLength() {

            HttpClient client = new HttpClient();

            var httpMessage = await client.GetAsync("http://appress.com");

            return httpMessage.Content.Headers.ContentLength;

        }
    }

    #endregion



}