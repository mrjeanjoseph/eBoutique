using KwiqBlog.BusinessManagers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KwiqBlog.Controllers {
    public class HomeController : Controller {
        private readonly IPostBusinessManager _postBusinessManager;
        private readonly IHomeBusinessManager _homeBusinessManager;

        public HomeController(
            IPostBusinessManager blogBusinessManager, 
            IHomeBusinessManager homeBusinessManager) {
            _postBusinessManager = blogBusinessManager;
            _homeBusinessManager = homeBusinessManager;
        }

        public IActionResult Index(string searchStr, int? page) {
            return View(_postBusinessManager.GetIndexViewModel(searchStr, page));
        }

        public IActionResult Author(string authorId, string str, int? page) {
            var actionResult = _homeBusinessManager.GetAuthorViewModel(authorId, str, page);

            if(actionResult.Result is null) return View(actionResult.Value);

            return View(actionResult.Result);
        }
    }
}
