using System.Linq;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;
using YTP.Domain.SportsStore.Entities;

namespace YTP.Main.Areas.SportsStore.Controllers {

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
    }
}