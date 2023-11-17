using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using YTP.Domain.SportsStore.Abstract;

namespace YTP.Main.Areas.SportsStore.Controllers {

    public class ProductController : Controller {
        private readonly IProductsRepository _productRepo;
        public int PageSize = 4;

        public ProductController(IProductsRepository productRepo) {
            _productRepo = productRepo;
        }

        // GET: SportsStore/Product
        public ActionResult ListProducts(int page = 1) {

            return View(_productRepo.Products
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize));
            //return View(_productRepo.Products);
        }
    }
}