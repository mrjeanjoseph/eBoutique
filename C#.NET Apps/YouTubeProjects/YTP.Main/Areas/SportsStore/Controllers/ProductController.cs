using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;

namespace YTP.Main.Areas.SportsStore.Controllers {

    public class ProductController : Controller {
        private readonly IProductsRepository _productRepo;

        public ProductController(IProductsRepository productRepo) {
            _productRepo = productRepo;
        }

        // GET: SportsStore/Product
        public ActionResult ListProducts() {

            return View(_productRepo.Products);
        }
    }
}