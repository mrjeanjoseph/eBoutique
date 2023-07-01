using KwiqBlog.Authorization;
using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data.Models;
using KwiqBlog.Models.BlogViewModels;
using KwiqBlog.Services;
using KwiqBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwiqBlog.BusinessManagers {
    public class BlogBusinessManager : IBlogBusinessManager {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly IAuthorizationService _authService;

        public BlogBusinessManager(UserManager<ApplicationUser> userManager, IBlogService blogService, IWebHostEnvironment webHostEnv, IAuthorizationService authService) {
            _userManager = userManager;
            _blogService = blogService;
            _webHostEnv = webHostEnv;
            _authService = authService;
        }

        public async Task<Blog> CreateBlog(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal) {
            Blog createdBlog = createViewModel.Blog;

            createdBlog.BlogCreator = await _userManager.GetUserAsync(claimsPrincipal);
            createdBlog.CreatedDate = DateTime.UtcNow;
            createdBlog.UpdatedDate = DateTime.UtcNow;

            createdBlog = await _blogService.Add(createdBlog);

            string webRootPath = _webHostEnv.WebRootPath;
            string pathToImg = $@"{webRootPath}\UserFiles\Blogs\{createdBlog.Id}\HeaderImg.png";

            DoesFolderExist(pathToImg);

            using (var fileSystem = new FileStream(pathToImg, FileMode.Create)) {
                await createViewModel.BlogHeaderImg.CopyToAsync(fileSystem);
            }
            return createdBlog;
        }

        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal) {
            if (id is null)
                return new BadRequestResult();

            var blogId = id.Value;
            var blog = _blogService.GetBlog(blogId);
            if (blog == null)
                return new NotFoundResult();

            var authResult = await _authService.AuthorizeAsync(claimsPrincipal, blog, BlogOperations.Update);
            if (!authResult.Succeeded) {
                if (claimsPrincipal.Identity.IsAuthenticated)
                    return new ForbidResult();
                else
                    return new ChallengeResult();
            }

            return new EditViewModel {
                Blog = blog,
            };
        }

        private void DoesFolderExist(string folderPath) {
            string dirName = Path.GetDirectoryName(folderPath);
            if (dirName.Length > 0) {
                Directory.CreateDirectory(Path.GetDirectoryName(folderPath));
            }
        }
    }
}
