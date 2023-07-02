using KwiqBlog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KwiqBlog.Services.Interfaces {
    public interface IBlogService {
        Blog GetBlog(int blogId);

        IEnumerable<Blog> GetBlogs(string searchString);

        IEnumerable<Blog> GetBlogs(ApplicationUser appUser);

        Task<Blog> Add(Blog blog);

        Task<Blog> Update(Blog blog);
    }
}
