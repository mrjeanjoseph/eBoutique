using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data.Models;
using KwiqBlog.Models.BlogViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwiqBlog.BusinessManagers
{
    public class BlogBusinessManager : IBlogBusinessManager
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogBusinessManager(UserManager<ApplicationUser> userManager)
        {
               this._userManager = userManager;
        }

        public async Task<Blog> CreateBlog(CreateBlogViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Blog createdBlog = createBlogViewModel.Blog;
            createdBlog.BlogCreator = await _userManager.GetUserAsync(claimsPrincipal);
            createdBlog.CreatedDate = DateTime.UtcNow;

            return createdBlog;
        }
    }
}
