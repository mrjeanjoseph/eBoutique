using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KwiqBlog.ViewComponents
{
    public class NavViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.Factory.StartNew(() => { return View(); });
        }
    }
}
