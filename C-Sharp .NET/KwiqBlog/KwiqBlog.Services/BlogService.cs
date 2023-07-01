using KwiqBlog.Data;
using KwiqBlog.Data.Models;
using KwiqBlog.Services.Interfaces;
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
    }
}
