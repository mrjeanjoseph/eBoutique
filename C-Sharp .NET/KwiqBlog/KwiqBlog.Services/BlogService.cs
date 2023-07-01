using KwiqBlog.Data;
using KwiqBlog.Data.Models;
using KwiqBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwiqBlog.Services
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext appDbContext;

        public BlogService(ApplicationDbContext dbConn)
        {
            this.appDbContext = dbConn;
        }
        public async Task<Blog> Add(Blog blog)
        {
            appDbContext.Add(blog);
            await appDbContext.SaveChangesAsync();
            return blog;
        }

        public IEnumerable<Blog> GetBlogs(ApplicationUser appUser)
        {
            return appDbContext.Blogs
                .Include(blog => blog.BlogCreator)
                .Include(blog => blog.Approver)
                .Include(blog => blog.Posts)
                .Where(blog => blog.BlogCreator == appUser);
        }
    }
}
