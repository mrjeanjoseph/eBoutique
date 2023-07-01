using KwiqBlog.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace KwiqBlog.Models.AdminViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
