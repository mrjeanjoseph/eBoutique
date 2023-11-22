using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YTP.Domain.SportsStore.Abstract;

namespace YTP.Main.Areas.SportsStore.Controllers {
    public class NavController : Controller {

        private readonly IProductsRepository _repository;

        public NavController(IProductsRepository repository) {
            _repository = repository;
        }
        
        public PartialViewResult Menu(string category = null) {
            //Menu(string category = null, bool horizontalMenu=false) 
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _repository.Products
                    .Select(p => p.Category)
                    .Distinct()
                    .OrderBy(p => p);

            //return PartialView(categories);
            //string menuLayout = horizontalMenu ? "_MenuHorizontal" : "_Menu";
            return PartialView("_Menu", categories);
        }
    }
}