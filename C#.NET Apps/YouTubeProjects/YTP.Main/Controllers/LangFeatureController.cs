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
    }
}