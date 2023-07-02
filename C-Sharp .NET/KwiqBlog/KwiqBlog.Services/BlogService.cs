using KwiqBlog.Data;
using KwiqBlog.Data.Models;
using KwiqBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwiqBlog.Services {
    public class BlogService : IBlogService {
        private readonly ApplicationDbContext _appDbContext;

        public BlogService(ApplicationDbContext dbConn) {
            _appDbContext = dbConn;
        }

        public Blog GetBlog(int blogId) {
            return _appDbContext.Blogs.FirstOrDefault(b => b.Id == blogId);
        }

        public IEnumerable<Blog> GetBlogs(string searchString) {
            return _appDbContext.Blogs
                .OrderByDescending(b => b.UpdatedDate)
                .Include(b => b.BlogCreator)
                .Include(b => b.Posts)
                .Where(b => b.Title.Contains(searchString) 
                || b.Content.Contains(searchString));
        }

        public IEnumerable<Blog> GetBlogs(ApplicationUser appUser) {
            return _appDbContext.Blogs
                .Include(blog => blog.BlogCreator)
                .Include(blog => blog.Approver)
                .Include(blog => blog.Posts)
                .Where(blog => blog.BlogCreator == appUser);
        }

        public async Task<Blog> Add(Blog blog) {
            _appDbContext.Add(blog);
            await _appDbContext.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> Update(Blog blog) {
            _appDbContext.Update(blog);
            await _appDbContext.SaveChangesAsync();
            return blog;
        }
    }
}
