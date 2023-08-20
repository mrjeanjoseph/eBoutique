using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class TBS_EmployeeController : Controller {
        // GET: TBS_Employee

        public ActionResult Index() {
            return View();
        }

        public ActionResult GetData() {

            using (DBContext dbAccess = new DBContext()) {

                List<TBS_Employee> empList = dbAccess.tbs_Employees.ToList<TBS_Employee>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0) {

            if(id == 0)
                return View(new TBS_Employee());
            else {
                using (DBContext dbAccess=new DBContext()) {
                    var result = dbAccess.tbs_Employees.Where(e => e.EmployeeId == id).FirstOrDefault<TBS_Employee>();

                    return View(result);
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(TBS_Employee empObj) {

            using (DBContext dbAccess = new DBContext()) {
                dbAccess.tbs_Employees.Add(empObj);
                dbAccess.SaveChanges();

                return Json(new { success = true, message = "Data entry saved successfully." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}