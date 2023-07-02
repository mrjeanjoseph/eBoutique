using KwiqBlog.Data.Models;
using KwiqBlog.Models.BlogViewModels;
using KwiqBlog.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KwiqBlog.BusinessManagers.Interfaces {
    public interface IBlogBusinessManager {
        IndexViewModel GetIndexViewModel(string searchStr, int? page);

        Task<Post> CreateBlog(CreateViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal);

        Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal);

        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
    }
}
