using KwiqBlog.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KwiqBlog.BusinessManagers.Interfaces {
    public interface IHomeBusinessManager {
        ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string str, int? page);
    }
}
