using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;

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
    }
}