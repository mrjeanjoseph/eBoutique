using System.Collections.Generic;
using YTP.Main.Models;
using System.Web.Mvc;
using System.Linq;

namespace YTP.Main.Controllers {
    public class ProductController : Controller {

        private ProductContext _dbContext;
        // GET: Product
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult GetProductDetails() {

            _dbContext = new ProductContext();

            List<ProductModel> listOfProducts = (from prodObj in _dbContext.Products
                            select new ProductModel {
                                ProductId = prodObj.ProductId,
                                Code = prodObj.Code,
                                Name = prodObj.Name,
                                Category = prodObj.Category,
                                Barcode = prodObj.Barcode,
                                StockQty = prodObj.StockQty,
                            }).ToList();

            return Json(listOfProducts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostProductDetails(ProductModel model) {

            _dbContext = new ProductContext();
            ProductModel product = new ProductModel();

            product.ProductId = model.ProductId;
            product.Code = model.Code;
            product.Name = model.Name;
            product.Category = model.Category;
            product.Barcode = model.Barcode;
            product.StockQty = model.StockQty;

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return Json(new {
                IsValid = true,
                Message = "Data Added Successfully!"
            }, JsonRequestBehavior.AllowGet);

        }
    }
}