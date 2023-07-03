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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostService _postService;

        public AdminBusinessManager(UserManager<ApplicationUser> userManager, IPostService postService)
        {
            _userManager = userManager;
            _postService = postService;
        }
        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);

            return new IndexViewModel
            {
                Posts = _postService.GetPosts(applicationUser),
            };
        }
    }
}
