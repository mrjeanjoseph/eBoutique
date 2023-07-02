using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data.Models;
using KwiqBlog.Models.AdminViewModels;
using KwiqBlog.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwiqBlog.BusinessManagers
{
    public class AdminBusinessManager : IAdminBusinessManager
    {
        private UserManager<ApplicationUser> _userManager;
        private IPostService _blogService;

        public AdminBusinessManager(UserManager<ApplicationUser> userManager, IPostService blogService)
        {
            _userManager = userManager;
            _blogService = blogService;
        }
        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);

            return new IndexViewModel
            {
                Blogs = _blogService.GetBlogs(applicationUser),
            };
        }
    }
}
