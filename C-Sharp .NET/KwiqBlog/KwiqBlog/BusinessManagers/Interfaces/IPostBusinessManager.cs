using KwiqBlog.Models.PostViewModels;
using KwiqBlog.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using KwiqBlog.Data.Models;

namespace KwiqBlog.BusinessManagers.Interfaces {
    public interface IPostBusinessManager {
        IndexViewModel GetIndexViewModel(string searchStr, int? page);

        Task<ActionResult<PostViewModel>> GetPostViewModel(int? id, ClaimsPrincipal claimsPrincipal);

        Task<Post> CreatePost(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal);

        Task<ActionResult<EditViewModel>> UpdatePost(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal);

        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
    }
}
