using System.Web.Mvc;
using YTP.Main.Areas.HaitiEmployee.DataAccess;
using YTP.Main.Areas.HaitiEmployee.Models;

namespace YTP.Main.Areas.HaitiEmployee.Controllers {
    public class HomeController : Controller
    {
        readonly EmployeeDB empDB = new EmployeeDB();
        // GET: Home  
        public ActionResult Index() {
            return View();
        }
        public JsonResult List() {
            return Json(empDB.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add(EmployeeModel emp) {
            return Json(empDB.Add(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID) {
            var Employee = empDB.ListAll().Find(x => x.EmployeeID.Equals(ID));
            return Json(Employee, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(EmployeeModel emp) {
            return Json(empDB.Update(emp), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID) {
            return Json(empDB.Delete(ID), JsonRequestBehavior.AllowGet);
        }
    }
}