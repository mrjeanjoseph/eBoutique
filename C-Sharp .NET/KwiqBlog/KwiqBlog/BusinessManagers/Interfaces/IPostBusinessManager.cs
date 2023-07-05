using KwiqBlog.Models.PostViewModels;
using KwiqBlog.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using KwiqBlog.Data.Models;

namespace KwiqBlog.BusinessManagers.Interfaces {
    public interface IPostBusinessManager {
        IndexViewModel GetIndexViewModel(string searchStr, int? page);

        Task<ActionResult<PostViewModel>> GetPostViewModel(int? id, ClaimsPrincipal principal);

        Task<Post> CreatePost(CreateViewModel viewModel, ClaimsPrincipal principal);

        Task<ActionResult<Comment>> CreateComment(PostViewModel viewModel, ClaimsPrincipal principal);

        Task<ActionResult<EditViewModel>> UpdatePost(EditViewModel viewModel, ClaimsPrincipal principal);

        Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal principal);
    }
}
