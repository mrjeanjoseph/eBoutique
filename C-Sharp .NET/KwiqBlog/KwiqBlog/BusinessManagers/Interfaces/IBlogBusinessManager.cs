using KwiqBlog.Data.Models;
using KwiqBlog.Models.BlogViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwiqBlog.BusinessManagers.Interfaces
{
    public interface IBlogBusinessManager
    {
        Task<Blog> CreateBlog(CreateViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal);
    }
}
