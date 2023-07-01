using KwiqBlog.Data;
using KwiqBlog.Data.Models;
using KwiqBlog.Services.Interfaces;
using System.Threading.Tasks;

namespace KwiqBlog.Services
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _dbConn;

        private BlogService(ApplicationDbContext dbConn)
        {
            _dbConn = dbConn;
        }
        public async Task<Blog> Add(Blog blog)
        {
            _dbConn.Add(blog);
            await _dbConn.SaveChangesAsync();
            return blog;
        }
    }
}
