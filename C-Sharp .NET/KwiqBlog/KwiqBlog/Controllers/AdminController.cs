using Microsoft.AspNetCore.Mvc;

namespace KwiqBlog.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
