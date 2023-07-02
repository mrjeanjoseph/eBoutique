using KwiqBlog.BusinessManagers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KwiqBlog.Controllers {
    public class HomeController : Controller {
        private readonly IPostBusinessManager _postBusinessManager;

        public HomeController(IPostBusinessManager blogBusinessManager) {
            _postBusinessManager = blogBusinessManager;
        }

        public IActionResult Index(string searchStr, int? page) {
            return View(_postBusinessManager.GetIndexViewModel(searchStr, page));
        }
    }
}
