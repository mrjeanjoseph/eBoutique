using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;

namespace YTP.Main.Areas.SportsStore.Controllers {

    [Authorize]
    public class AdminController : Controller {

        private readonly IProductsRepository _productRepo;

        public AdminController(IProductsRepository products) {            
            _productRepo = products;
        }

        // GET: SportsStore/Admin
        public ViewResult Index() {
            return View(_productRepo.Products);
        }

        public ViewResult Edit(int productId) {
            Product product = _productRepo.Products
                .FirstOrDefault(p => p.ProductID == productId);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null) {

            if(ModelState.IsValid) {
                if(image != null) {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0 , image.ContentLength);
                }
            }

            if(ModelState.IsValid) {
                _productRepo.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.ProductName);
                return RedirectToAction("Index");
            } else {
                // There is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create() {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId) {
            Product deletedProduct = _productRepo.DeleteProduct(productId);

            if(deletedProduct != null) 
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.ProductName);

            return RedirectToAction("Index");
            
        }
    }
}