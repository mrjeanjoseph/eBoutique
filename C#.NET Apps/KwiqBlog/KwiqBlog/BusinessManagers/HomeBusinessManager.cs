using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data.Models;
using KwiqBlog.Models.HomeViewModels;
using KwiqBlog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System.Linq;

namespace KwiqBlog.BusinessManagers {
    public class HomeBusinessManager : IHomeBusinessManager {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public HomeBusinessManager(
            IPostService postService, 
            IUserService userService) {
            _postService = postService;
            _userService = userService;
        }

        public ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string str, int? page) {
            if (authorId is null) return new BadRequestResult();

            var appUser = _userService.Get(authorId);

            if(appUser is null) return new NotFoundResult();

            int pageSize = 20;
            int pageNumber = page ?? 1;

            var posts = _postService.GetPosts(str ?? string.Empty)
                .Where(p => p.Published && p.PostCreator == appUser);

            return new AuthorViewModel {
                Author = appUser,
                Posts = new StaticPagedList<Post>(posts.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, posts.Count()),
                SearchString = str,
                PageNumber = pageNumber
            };

        }

    }
}
