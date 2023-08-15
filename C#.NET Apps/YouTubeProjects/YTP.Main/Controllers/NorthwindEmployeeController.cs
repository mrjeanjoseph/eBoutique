using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class NorthwindEmployeeController : Controller
    {
        // GET: NorthwindEmployee
        public ActionResult Index()
        {
            DbAccess dbhandle = new DbAccess();
            ModelState.Clear();
            return View(dbhandle.GetNorthwindEmployees());
        }
    }
}