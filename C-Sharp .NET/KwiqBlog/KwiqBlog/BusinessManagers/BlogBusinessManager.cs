using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data.Models;
using KwiqBlog.Models.BlogViewModels;
using KwiqBlog.Services;
using KwiqBlog.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwiqBlog.BusinessManagers
{
    public class BlogBusinessManager : IBlogBusinessManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _webHostEnv;

        public BlogBusinessManager(UserManager<ApplicationUser> userManager, IBlogService blogService, IWebHostEnvironment webHostEnv)
        {
            this._userManager = userManager;
            this._blogService = blogService;
            this._webHostEnv = webHostEnv;
        }

        public async Task<Blog> CreateBlog(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Blog createdBlog = createViewModel.Blog;

            createdBlog.BlogCreator = await _userManager.GetUserAsync(claimsPrincipal);
            createdBlog.CreatedDate = DateTime.UtcNow;
            createdBlog.UpdatedDate = DateTime.UtcNow;

            createdBlog = await _blogService.Add(createdBlog);

            string webRootPath = _webHostEnv.WebRootPath;
            string pathToImg = $@"{webRootPath}\UserFiles\Blogs\{createdBlog.Id}\HeaderImg.png";

            DoesFolderExist(pathToImg);

            using(var fileSystem = new FileStream(pathToImg, FileMode.Create))
            {
                await createViewModel.BlogHeaderImg.CopyToAsync(fileSystem);
            }
            return createdBlog;


        }

        private void DoesFolderExist(string folderPath)
        {
            string dirName = Path.GetDirectoryName(folderPath);
            if(dirName.Length > 0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(folderPath));
            }
        }
    }
}
