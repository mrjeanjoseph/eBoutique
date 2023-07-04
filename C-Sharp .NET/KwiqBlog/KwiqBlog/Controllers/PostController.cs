using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Models.PostViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KwiqBlog.Controllers {

    [Authorize]
    public class PostController : Controller {
        private readonly IPostBusinessManager _postBusinessManager;

        public PostController(IPostBusinessManager postBusinessManager) {
            _postBusinessManager = postBusinessManager;
        }

        [Route("Post/{id}"), AllowAnonymous]
        public async Task<IActionResult> Index(int? id) {
            var indexResult = await _postBusinessManager.GetPostViewModel(id, User);

            if (indexResult.Result is null)
                return View(indexResult.Value);

            return indexResult.Result;
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
            return RedirectToAction("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel viewModel) {
            var updateResult =  await _postBusinessManager.UpdatePost(viewModel, User);

            if (updateResult.Result is null)
                //return RedirectToAction("Index");
                return RedirectToAction("Edit", new { viewModel.Post.Id });

            return updateResult.Result;
        }
    }
}
