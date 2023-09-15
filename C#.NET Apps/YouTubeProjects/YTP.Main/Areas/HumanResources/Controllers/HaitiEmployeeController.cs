using System.Web.Mvc;
using YTP.Main.Areas.HumanResources.DataAccess;
using YTP.Main.Areas.HumanResources.Models;

namespace YTP.Main.Areas.HumanResources.Controllers {
    public class HaitiEmployeeController : Controller
    {
        readonly HaitiEmployeeDB empDB = new HaitiEmployeeDB();
        // GET: Home  
        public ActionResult Index() {
            return View();
        }
        public JsonResult List() {
            return Json(empDB.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(HaitiEmployeeModel emp) {
            return Json(empDB.Add(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID) {
            var Employee = empDB.ListAll().Find(x => x.EmployeeID.Equals(ID));
            return Json(Employee, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(HaitiEmployeeModel emp) {
            return Json(empDB.Update(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID) {
            return Json(empDB.Delete(ID), JsonRequestBehavior.AllowGet);
        }
    }
}