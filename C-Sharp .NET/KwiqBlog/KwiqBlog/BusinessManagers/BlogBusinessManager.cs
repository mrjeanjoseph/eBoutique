using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data.Models;
using KwiqBlog.Models.BlogViewModels;
using KwiqBlog.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwiqBlog.BusinessManagers
{
    public class BlogBusinessManager : IBlogBusinessManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogService _blogService;

        public BlogBusinessManager(UserManager<ApplicationUser> userManager, IBlogService blogService)
        {
               this._userManager = userManager;
               this._blogService = blogService;
        }

        public async Task<Blog> CreateBlog(CreateBlogViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Blog createdBlog = createBlogViewModel.Blog;
            createdBlog.BlogCreator = await _userManager.GetUserAsync(claimsPrincipal);
            createdBlog.CreatedDate = DateTime.UtcNow;

            return await _blogService.Add(createdBlog);
        }
    }
}
