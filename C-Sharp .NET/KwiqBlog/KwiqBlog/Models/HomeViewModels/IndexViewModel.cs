using KwiqBlog.Data.Models;
using PagedList;

namespace KwiqBlog.Models.HomeViewModels {
    public class IndexViewModel {
        public IPagedList<Blog> Blogs { get ; set; }
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
    }
}
