using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data.Models;
using KwiqBlog.Models.AdminViewModels;
using KwiqBlog.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwiqBlog.BusinessManagers {
    public class AdminBusinessManager : IAdminBusinessManager {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnv;

        public AdminBusinessManager(
            UserManager<ApplicationUser> userManager,
            IPostService postService,
            IUserService userService,
            IWebHostEnvironment webHostEnv) {
            _userManager = userManager;
            _postService = postService;
            _userService = userService;
            _webHostEnv = webHostEnv;
        }
        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal) {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);

            return new IndexViewModel {
                Posts = _postService.GetPosts(applicationUser),
            };
        }
    }
}
