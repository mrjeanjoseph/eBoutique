using KwiqBlog.BusinessManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KwiqBlog.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminBusinessManager _adminBusinessManager;

        public AdminController(IAdminBusinessManager adminBusinessManager)
        {
            _adminBusinessManager = adminBusinessManager;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _adminBusinessManager.GetAdminDashboard(User));
        }
    }
}
