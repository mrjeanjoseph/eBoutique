using KwiqBlog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KwiqBlog.Services.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> Add(Blog blog);
        public IEnumerable<Blog> GetBlogs(ApplicationUser appUser);
    }
}
