using KwiqBlog.Data.Models;
using KwiqBlog.Models.BlogViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwiqBlog.BusinessManagers.Interfaces
{
    public interface IBlogBusinessManager
    {
        Task<Blog> CreateBlog(CreateViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
    }
}
