using KwiqBlog.Data;
using KwiqBlog.Data.Models;
using KwiqBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
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
            _appDbContext.Add(blog);
            await _appDbContext.SaveChangesAsync();
            return blog;
        }
    }
}
