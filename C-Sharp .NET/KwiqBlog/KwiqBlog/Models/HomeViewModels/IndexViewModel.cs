using KwiqBlog.Data.Models;
using PagedList;
using PagedList.Core;

namespace KwiqBlog.Models.HomeViewModels {
    public class IndexViewModel {
        public IPagedList<Blog> Blogs { get ; set; }
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
    }
}
