using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KwiqBlog.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Factory.StartNew(() => { return View(); });
        }
    }
}
