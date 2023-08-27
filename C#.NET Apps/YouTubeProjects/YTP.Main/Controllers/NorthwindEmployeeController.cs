using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class NorthwindEmployeeController : Controller
    {
        // GET: NorthwindEmployee
        public ActionResult Index()
        {
            Sp_EmpDBAccess dbhandle = new Sp_EmpDBAccess();
            ModelState.Clear();
            return View(dbhandle.GetNorthwindEmployees());
        }
    }
}