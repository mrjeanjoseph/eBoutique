﻿using KwiqBlog.Data.Models;
using PagedList.Core;

namespace KwiqBlog.Models.HomeViewModels {
    public class AuthorViewModel {
        public ApplicationUser Author { get; set; }
        public IPagedList<Post> Posts { get; set; }
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
    }
}
