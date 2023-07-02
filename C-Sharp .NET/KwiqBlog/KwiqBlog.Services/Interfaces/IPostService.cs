using KwiqBlog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KwiqBlog.Services.Interfaces {
    public interface IPostService {
        Post GetPost(int postId);

        IEnumerable<Post> GetPosts(string str);

        IEnumerable<Post> GetPosts(ApplicationUser appUser);

        Task<Post> Add(Post post);

        Task<Post> Update(Post post);
    }
}
