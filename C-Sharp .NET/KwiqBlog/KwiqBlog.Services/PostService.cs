﻿using Microsoft.EntityFrameworkCore;
using KwiqBlog.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using KwiqBlog.Data.Models;
using KwiqBlog.Data;
using System.Linq;

namespace KwiqBlog.Services {
    public class PostService : IPostService {
        private readonly ApplicationDbContext _appDbContext;

        public PostService(ApplicationDbContext dbConn) {
            _appDbContext = dbConn;
        }

        public Post GetPost(int postId) {
            return _appDbContext.Posts
                .Include(p => p.PostCreator)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Commentor)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Comments)
                .FirstOrDefault(b => b.Id == postId);
        }

        public IEnumerable<Post> GetPosts(string str) {
            return _appDbContext.Posts
                .OrderByDescending(p => p.UpdatedDate)
                .Include(p => p.PostCreator)
                .Include(p => p.Comments)
                .Where(p => p.Title.Contains(str) 
                || p.Content.Contains(str));
        }

        public IEnumerable<Post> GetPosts(ApplicationUser appUser) {
            return _appDbContext.Posts
                .Include(p => p.PostCreator)
                .Include(p => p.Approver)
                .Include(p => p.Comments)
                .Where(p => p.PostCreator == appUser);
        }

        public async Task<Post> Add(Post post) {
            _appDbContext.Add(post);
            await _appDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Post> Update(Post post) {
            _appDbContext.Update(post);
            await _appDbContext.SaveChangesAsync();
            return post;
        }
    }
}
