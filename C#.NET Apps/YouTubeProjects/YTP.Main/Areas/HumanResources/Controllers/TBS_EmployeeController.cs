using YTP.Main.DataAccess;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace YTP.Main.Areas.HumanResources.Controllers {
    public class TBS_EmployeeController : Controller {
        // GET: TBS_Employee

        private readonly DBContext dbAccess = new DBContext();

        public ActionResult Index() {

            return View();
        }

        public ActionResult GetData() {

            List<TBS_Employee> empList = dbAccess.TBS_Employee.ToList<TBS_Employee>();
            return Json(new { data = empList }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0) {

            if (id == 0)
                return View(new TBS_Employee());
            else {

                var result = dbAccess.TBS_Employee.Where(e => e.EmployeeId == id).FirstOrDefault<TBS_Employee>();
                return View(result);

            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(TBS_Employee empObj) {

            if (empObj.EmployeeId == 0) { // If it is 0, it must be adding new obj

                dbAccess.TBS_Employee.Add(empObj);
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

            TBS_Employee empOjb = dbAccess.TBS_Employee.Where(e => e.EmployeeId == id).FirstOrDefault<TBS_Employee>();
            var result = empOjb.FullName.Length;
            dbAccess.TBS_Employee.Remove(empOjb);
            dbAccess.SaveChanges();
            

            return Json(new { success = true, message = "Data Deleted successfully." }, JsonRequestBehavior.AllowGet);

        }
    }
}