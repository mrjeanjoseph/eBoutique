using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Models.BlogViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KwiqBlog.Controllers {
    public class BlogController : Controller {
        private readonly IBlogBusinessManager _blogBusinessManager;

        public BlogController(IBlogBusinessManager blogBusinessManager) {
            _blogBusinessManager = blogBusinessManager;
        }
        public IActionResult Index() {
            return View();
        }

        public IActionResult Create() {
            return View(new CreateViewModel());
        }

        public async Task<IActionResult> Edit(int? id) {
            var editResult = await _blogBusinessManager.GetEditViewModel(id, User);
            if (editResult.Result is null)
                return View(editResult.Value);

            return editResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createblogViewModel) {
            await _blogBusinessManager.CreateBlog(createblogViewModel, User);
            return RedirectToAction("Create");
        }
    }
}
