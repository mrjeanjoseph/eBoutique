using System.Web.Mvc;

namespace YTP.Main.Areas.HumanResources.Controllers {
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