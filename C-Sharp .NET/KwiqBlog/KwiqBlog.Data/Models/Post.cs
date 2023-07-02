using System;
using System.Collections.Generic;
using System.Text;

namespace KwiqBlog.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public ApplicationUser PostCreator { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Published { get; set; }

        public bool Approved { get; set; }
        public ApplicationUser Approver { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
