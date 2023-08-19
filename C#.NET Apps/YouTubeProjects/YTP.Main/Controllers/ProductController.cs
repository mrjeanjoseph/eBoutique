using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class ProductController : Controller {

        ProductTblEntities _prodOject;
        // GET: Product
        public ActionResult Index() {
            return View();
        }

        [HttpGet]
        public ActionResult GetProductDetails() {

            _prodOject = new ProductTblEntities();
            List<ProductModel> products = (from obj in _prodOject.Products
                        select new ProductModel {
                            ProductId = obj.ProductId,
                            Code = obj.Code,
                            Name = obj.Name,
                            Category = obj.Category,
                            Barcode = obj.Barcode,
                            StockQty = obj.StockQty,
                        }).ToList();

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel objModel) {

            _prodOject = new ProductTblEntities();

            Product product = new Product();

            product.ProductId = objModel.ProductId;
            product.Code = objModel.Code;
            product.Name = objModel.Name;
            product.Category = objModel.Category;
            product.Barcode = objModel.Barcode;
            product.StockQty = objModel.StockQty;

            _prodOject.Products.Add(product);
            _prodOject.SaveChanges();

            return Json(new {
                IsValid = true,
                Message = "Data Successfully Added"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}