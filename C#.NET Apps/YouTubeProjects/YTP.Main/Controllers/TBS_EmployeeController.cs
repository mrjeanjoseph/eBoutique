using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class TBS_EmployeeController : Controller {
        // GET: TBS_Employee

        private readonly DBContext dbAccess = new DBContext();

        public ActionResult Index() {

            return View();
        }

        public ActionResult GetData() {

            List<TBS_Employee> empList = dbAccess.tbs_Employees.ToList<TBS_Employee>();
            return Json(new { data = empList }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0) {

            if (id == 0)
                return View(new TBS_Employee());
            else {

                var result = dbAccess.tbs_Employees.Where(e => e.EmployeeId == id).FirstOrDefault<TBS_Employee>();
                return View(result);

            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(TBS_Employee empObj) {

            if (empObj.EmployeeId == 0) { // If it is 0, it must be adding new obj

                dbAccess.tbs_Employees.Add(empObj);
                dbAccess.SaveChanges();
                return Json(new { success = true, message = "Data entry saved successfully." }, JsonRequestBehavior.AllowGet);

            } else {

                dbAccess.Entry(empObj).State = EntityState.Modified;
                dbAccess.SaveChanges();
                return Json(new { success = true, message = "Data modified and updated successfully." }, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpPost]
        public ActionResult Delete(int id) {

            TBS_Employee empOjb = dbAccess.tbs_Employees.Where(e => e.EmployeeId == id).FirstOrDefault<TBS_Employee>();
            dbAccess.tbs_Employees.Remove(empOjb);
            dbAccess.SaveChanges();

            return Json(new { success = true, message = "Data Deleted successfully." }, JsonRequestBehavior.AllowGet);

        }
    }
}