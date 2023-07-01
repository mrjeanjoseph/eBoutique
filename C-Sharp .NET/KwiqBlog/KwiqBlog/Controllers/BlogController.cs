using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Models.BlogViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KwiqBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogBusinessManager blogBusinessManager;

        public BlogController(IBlogBusinessManager blogBusinessManager)
        {
            this.blogBusinessManager = blogBusinessManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createblogViewModel)
        {
            await blogBusinessManager.CreateBlog(createblogViewModel, User);
            return RedirectToAction("Create");
        }
    }
}
