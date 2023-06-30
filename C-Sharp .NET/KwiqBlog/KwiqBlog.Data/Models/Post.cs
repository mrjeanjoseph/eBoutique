using System;
using System.Collections.Generic;
using System.Text;

namespace KwiqBlog.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public Blog Blog { get; set; }
        public ApplicationUser PostCreator { get; set; }
        public string Content { get; set; }
        public Post Parent { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
