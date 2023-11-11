using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class LangFeatureController : Controller {
        // GET: LangFeature
        public ActionResult Index() {

            return View();
        }

        public string Index2() {
            return "Navigate to a URL to show example";
        }

        public ViewResult AutoProperty() {
            //Create a new product object
            Product myprod = new Product();

            //Set the property value
            myprod.Name = "Kannot";

            //Get the property
            string prodName = myprod.Name;

            //Generate the view
            return View("Result", (object)String.Format("Product name: {0}", prodName));
        }

        public ViewResult CreateProduct() {
            //Create a new Product Oject
            Product newProduct = new Product {
                //Set the property values
                ProductID = 1005,
                Name = "Bwa Kayiman",
                Description = "Youn nan bel chose Ayiti",
                ProductPrice = 9540,
                Category = "Monument"
            };

            //Generate the view
            return View("Result", 
                (object)String.Format("Product name: {0}\n Product Description: {1}", 
                newProduct.Name, 
                newProduct.Description));
        }

        public ActionResult CreateCollectionExample() {
            string[] FavCities = { "Lascahobas", "Bon Repos", "Saint Michel de l'Attalaye", "Jeremie" };
            List<int> cityKeys = new List<int> { 10, 20, 30, 40 };

            Dictionary<string, int> cityList = new Dictionary<string, int>();
            int counting = 0;
            string result = "";

            foreach (var item in FavCities) {

                result += item.ToString() + ", ";

                cityList.Add(item, cityKeys[counting + 1]);
            }

            return View("Result", (object)result);
        }

        public ActionResult UseExtension() {

            //Create and Populate Shopping Cart
            ShoppingCart cart = new ShoppingCart() {
                Products = new List<Product>() {
                    new Product { Name = "Milet", ProductPrice = 950M},
                    new Product { Name = "Cheval", ProductPrice = 1550M},
                    new Product { Name = "Manman Bef", ProductPrice = 2350M},
                    new Product { Name = "Bouret", ProductPrice = 210M},
                    new Product { Name = "Chay Bannann", ProductPrice = 95M},
                }
            };

            //Get the total value of the product in the cart
            decimal cartTotal = cart.TotalPrices();
            return View("Result", (object)String.Format("Total: {0:c}", cartTotal));
        }

        public ActionResult UseExtensionEnum() {

            //Create and Populate a normal Object
            IEnumerable<Product> productList = new ShoppingCart2 {
                ProductEnum = new List<Product>() {
                    new Product { Name = "Milet", ProductPrice = 950M},
                    new Product { Name = "Cheval", ProductPrice = 1550M},
                    new Product { Name = "Manman Bef", ProductPrice = 2350M},
                    new Product { Name = "Bouret", ProductPrice = 210M},
                    new Product { Name = "Chay Bannann", ProductPrice = 95M},
                }
            };

            //Create and Populate an array of Product Objects
            Product[] productArray = {
                    new Product { Name = "Kana", ProductPrice = 121M},
                    new Product { Name = "Poul", ProductPrice = 102M},
                    new Product { Name = "Vyann Bef", ProductPrice = 97M},
                    new Product { Name = "Ze", ProductPrice = 38M},
                    new Product { Name = "Plan Banann", ProductPrice = 45M},
            };

            //Get the total value of the product in the cart
            decimal prodListTotal = productList.TotalPrices();
            decimal prodArrayTotal = productArray.TotalPrices();

            return View("Result", (object)String.Format("List Total: {0:c} -  Array Total: {1:c}", 
                prodListTotal, 
                prodArrayTotal));
        }
        
        public ViewResult UseFilterExtensionMethod() {

            IEnumerable<Product> product = new ShoppingCart2 {
                ProductEnum = new List<Product>() {
                    new Product { Name = "Milet", Category = "Travay", ProductPrice = 950M},
                    new Product { Name = "Bouret", Category = "Mason", ProductPrice = 210M},
                    new Product { Name = "Cheval", Category = "Travay", ProductPrice = 1550M},
                    new Product { Name = "Manman Bef", Category = "Travay", ProductPrice = 2350M},
                    new Product { Name = "Chay Bannann", Category = "Rekot", ProductPrice = 95M},
                }
            };

            decimal total = 0;
            foreach(Product prod in product.FilterByCategory("Travay")) {
                total += prod.ProductPrice;
            }

            return View("Result", (object)String.Format("Total: {0:c}", total));
        }
    }
}