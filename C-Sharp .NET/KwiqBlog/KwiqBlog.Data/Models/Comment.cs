using System.Collections.Generic;
using System;

namespace KwiqBlog.Data.Models {
    public class Comment {
        public int Id { get; set; }
        public Post Post { get; set; }
        public ApplicationUser Commentor { get; set; }
        public string Content { get; set; }
        public Comment Parent { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }

    }
}
