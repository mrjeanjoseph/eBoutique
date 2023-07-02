using KwiqBlog.Authorization;
using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data.Models;
using KwiqBlog.Models.BlogViewModels;
using KwiqBlog.Models.HomeViewModels;
using KwiqBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwiqBlog.BusinessManagers {
    public class BlogBusinessManager : IBlogBusinessManager {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly IAuthorizationService _authService;

        public BlogBusinessManager(
            UserManager<ApplicationUser> userManager, 
            IBlogService blogService, 
            IWebHostEnvironment webHostEnv, 
            IAuthorizationService authService) {
            _userManager = userManager;
            _blogService = blogService;
            _webHostEnv = webHostEnv;
            _authService = authService;
        }

        public IndexViewModel GetIndexViewModel(string searchStr, int? page) {
            int pageSize = 20;
            int pageNumber = page ?? 1;
            var blogs = _blogService.GetBlogs(searchStr ?? string.Empty);

            return new IndexViewModel {
                Blogs = new StaticPagedList<Blog>(blogs.Skip((pageNumber - 1) * pageSize), pageNumber, pageSize, blogs.Count()),
                SearchString = searchStr,
                PageNumber = pageNumber
            };
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

        public async Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal) {
            var updatedBlog = _blogService.GetBlog(editViewModel.Blog.Id);

            if (updatedBlog is null)
                return new NotFoundResult();
            var authResult = await _authService.AuthorizeAsync(claimsPrincipal, updatedBlog, BlogOperations.Update);
            if (!authResult.Succeeded) return CheckeActionResult(claimsPrincipal);

            updatedBlog.Published = editViewModel.Blog.Published;
            updatedBlog.Title = editViewModel.Blog.Title;
            updatedBlog.Content = editViewModel.Blog.Content;
            updatedBlog.UpdatedDate = DateTime.UtcNow;

            //Header img is option, so we will check if they want to upload a new img
            if (editViewModel.BlogHeaderImg != null) {
                string webRootPath = _webHostEnv.WebRootPath;
                string pathToImg = $@"{webRootPath}\UserFiles\Blogs\{updatedBlog.Id}\HeaderImg.png";

                DoesFolderExist(pathToImg);
                using (var fileSystem = new FileStream(pathToImg, FileMode.Create)) {
                    await editViewModel.BlogHeaderImg.CopyToAsync(fileSystem);
                }

            }

            //If we make it this far
            return new EditViewModel {
                Blog = await _blogService.Update(updatedBlog)
            };

        }

        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal) {
            if (id is null)
                return new BadRequestResult();

            var blogId = id.Value;
            var blog = _blogService.GetBlog(blogId);
            if (blog == null)
                return new NotFoundResult();

            var authResult = await _authService.AuthorizeAsync(claimsPrincipal, blog, BlogOperations.Update);

            if (!authResult.Succeeded) return CheckeActionResult(claimsPrincipal);

            return new EditViewModel {
                Blog = blog,
            };
        }

        private ActionResult CheckeActionResult(ClaimsPrincipal claimsPrincipal) {
            if (claimsPrincipal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }

        private void DoesFolderExist(string folderPath) {
            string dirName = Path.GetDirectoryName(folderPath);
            if (dirName.Length > 0) {
                Directory.CreateDirectory(Path.GetDirectoryName(folderPath));
            }
        }
    }
}
