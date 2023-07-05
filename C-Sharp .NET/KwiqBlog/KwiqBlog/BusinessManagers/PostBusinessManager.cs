using KwiqBlog.Authorization;
using KwiqBlog.BusinessManagers.Interfaces;
using KwiqBlog.Data.Models;
using KwiqBlog.Models.HomeViewModels;
using KwiqBlog.Models.PostViewModels;
using KwiqBlog.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using PagedList.Core;
using System.Linq;
using System.IO;
using System;

namespace KwiqBlog.BusinessManagers {
    public class PostBusinessManager : IPostBusinessManager {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostService _postService;
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly IAuthorizationService _authService;

        public PostBusinessManager(
            UserManager<ApplicationUser> userManager,
            IPostService postService,
            IWebHostEnvironment webHostEnv,
            IAuthorizationService authService) {
            _userManager = userManager;
            _postService = postService;
            _webHostEnv = webHostEnv;
            _authService = authService;
        }

        public IndexViewModel GetIndexViewModel(string str, int? page) {
            int pageSize = 20;
            int pageNumber = page ?? 1;
            var posts = _postService.GetPosts(str ?? string.Empty)
                .Where(b => b.Published);

            return new IndexViewModel {
                Posts = new StaticPagedList<Post>(posts.Skip((pageNumber - 1) * pageSize).Take(pageSize), pageNumber, pageSize, posts.Count()),
                SearchString = str,
                PageNumber = pageNumber
            };
        }

        public async Task<ActionResult<PostViewModel>> GetPostViewModel(int? id, ClaimsPrincipal principal) {
            if (id is null) return new BadRequestResult();

            var postId = id.Value;
            var post = _postService.GetPost(postId);

            if (post is null) return new NotFoundResult();

            if (!post.Published) {
                var authResult = await _authService.AuthorizeAsync(principal, post, PostOperations.Read);

                if (!authResult.Succeeded) return CheckeActionResult(principal);
            }

            return new PostViewModel {
                Post = post,
            };
        }

        public async Task<Post> CreatePost(CreateViewModel createViewModel, ClaimsPrincipal principal) {
            Post createdPost = createViewModel.Post;

            createdPost.PostCreator = await _userManager.GetUserAsync(principal);
            createdPost.CreatedDate = DateTime.UtcNow;
            createdPost.UpdatedDate = DateTime.UtcNow;

            createdPost = await _postService.Add(createdPost);

            string webRootPath = _webHostEnv.WebRootPath;
            string pathToImg = $@"{webRootPath}\UserFiles\Posts\{createdPost.Id}\HeaderImg.png";

            DoesFolderExist(pathToImg);

            using (var fileSystem = new FileStream(pathToImg, FileMode.Create)) {
                await createViewModel.HeaderImg.CopyToAsync(fileSystem);
            }
            return createdPost;
        }

        public async Task<ActionResult<Comment>> CreateComment(PostViewModel viewModel, ClaimsPrincipal principal) {
            if (viewModel.Post is null || viewModel.Post.Id == 0) return new BadRequestResult();

            var post = _postService.GetPost(viewModel.Post.Id);

            if(post is null) return new NotFoundResult();

            var comment = viewModel.Comment;

            comment.Commentor = await _userManager.GetUserAsync(principal);
            comment.Post = post;
            comment.CreateDate = DateTime.Now;

            if(comment.Parent != null) comment.Parent = _postService.GetComment(comment.Parent.Id);

            return await _postService.Add(comment);
        }

        public async Task<ActionResult<EditViewModel>> UpdatePost(EditViewModel editViewModel, ClaimsPrincipal principal) {
            var updatedPost = _postService.GetPost(editViewModel.Post.Id);

            if (updatedPost is null)
                return new NotFoundResult();
            var authResult = await _authService.AuthorizeAsync(principal, updatedPost, PostOperations.Update);
            if (!authResult.Succeeded) return CheckeActionResult(principal);

            updatedPost.Published = editViewModel.Post.Published;
            updatedPost.Title = editViewModel.Post.Title;
            updatedPost.Content = editViewModel.Post.Content;
            updatedPost.UpdatedDate = DateTime.UtcNow;

            //Header img is option, so we will check if they want to upload a new img
            if (editViewModel.HeaderImg != null) {
                string webRootPath = _webHostEnv.WebRootPath;
                string pathToImg = $@"{webRootPath}\UserFiles\Posts\{updatedPost.Id}\HeaderImg.png";

                DoesFolderExist(pathToImg);
                using (var fileSystem = new FileStream(pathToImg, FileMode.Create)) {
                    await editViewModel.HeaderImg.CopyToAsync(fileSystem);
                }

            }

            //If we make it this far
            return new EditViewModel {
                Post = await _postService.Update(updatedPost)
            };

        }

        public async Task<ActionResult<EditViewModel>> GetEditViewModel(int? id, ClaimsPrincipal principal) {
            if (id is null)
                return new BadRequestResult();

            var postId = id.Value;
            var post = _postService.GetPost(postId);
            if (post == null)
                return new NotFoundResult();

            var authResult = await _authService.AuthorizeAsync(principal, post, PostOperations.Update);

            if (!authResult.Succeeded) return CheckeActionResult(principal);

            return new EditViewModel {
                Post = post,
            };
        }

        private ActionResult CheckeActionResult(ClaimsPrincipal principal) {
            if (principal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }

        private void DoesFolderExist(string folderPath) {
            string dirName = Path.GetDirectoryName(folderPath);
            if (dirName.Length > 0) {
                Directory.CreateDirectory(Path.GetDirectoryName(folderPath));
            }
        }
    }
}
