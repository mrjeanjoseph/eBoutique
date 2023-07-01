using KwiqBlog.Data.Models;
using System.Threading.Tasks;

namespace KwiqBlog.Services.Interfaces
{
    public interface IBlogService
    {
        Task<Blog> Add(Blog blog);
    }
}
