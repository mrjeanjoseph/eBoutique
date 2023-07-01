using KwiqBlog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KwiqBlog.Services.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> Add(Blog blog);
        IEnumerable<Blog> GetBlogs(ApplicationUser appUser);
        Blog GetBlog(int blogId);
    }
}
