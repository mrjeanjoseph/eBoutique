using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data.Models;
using KwiqBlog.Models.AdminViewModels;
using KwiqBlog.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.IO;
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
        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claims) {
            var appUser = await _userManager.GetUserAsync(claims);

            return new IndexViewModel {
                Posts = _postService.GetPosts(appUser),
            };
        }
        public async Task<AboutViewModel> GetAboutViewModel(ClaimsPrincipal claims) {
            var appUser = await _userManager.GetUserAsync(claims);
            return new AboutViewModel {
                AppUser = appUser,
                SubHeader = appUser.SubHeader,
                Content = appUser.AboutContent,
            };
        }

        public async Task UpdateAbout(AboutViewModel viewModel, ClaimsPrincipal claims) {
            var appUser = await _userManager.GetUserAsync(claims);

            appUser.SubHeader = viewModel.SubHeader;
            appUser.AboutContent = viewModel.Content;

            if (viewModel.HeaderImg != null) {
                string webrootPath = _webHostEnv.WebRootPath;
                string pathToImg = $@"{webrootPath}\UserFiles\Users\{appUser.Id}\Header-Img.png";

                DoesFolderExist(pathToImg);

                using (var fs = new FileStream(pathToImg, FileMode.Create))
                    await viewModel.HeaderImg.CopyToAsync(fs);
            }

            await _userService.Update(appUser);
        }

        private void DoesFolderExist(string folderPath) {
            string dirName = Path.GetDirectoryName(folderPath);
            if (dirName.Length > 0)
                Directory.CreateDirectory(Path.GetDirectoryName(folderPath));
            
        }
    }
}
