using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Models.PostViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KwiqBlog.Controllers {
    public class PostController : Controller {
        private readonly IPostBusinessManager _postBusinessManager;

        public PostController(IPostBusinessManager postBusinessManager) {
            _postBusinessManager = postBusinessManager;
        }
        public IActionResult Index() {
            return View();
        }

        public IActionResult Create() {
            return View(new CreateViewModel());
        }

        public async Task<IActionResult> Edit(int? id) {
            var editResult = await _postBusinessManager.GetEditViewModel(id, User);
            if (editResult.Result is null)
                return View(editResult.Value);

            return editResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel viewModel) {
            await _postBusinessManager.CreatePost(viewModel, User);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel viewModel) {
            var updateResult =  await _postBusinessManager.UpdatePost(viewModel, User);

            if (updateResult.Result is null)
                return RedirectToAction("Index");
            //return RedirectToAction("Edit", new { viewModel.Blog.Id });

            return updateResult.Result;
        }
    }
}
