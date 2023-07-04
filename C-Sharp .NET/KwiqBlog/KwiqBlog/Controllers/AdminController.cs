using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Models.AdminViewModels;
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

        public async Task<IActionResult> About() {
            return View(await _adminBusinessManager.GetAboutViewModel(User));
        }

        public async Task<IActionResult> UpdateAbout(AboutViewModel viewModel) {
            await _adminBusinessManager.UpdateAbout(viewModel, User);
            return RedirectToAction("About");
        }
    }
}
