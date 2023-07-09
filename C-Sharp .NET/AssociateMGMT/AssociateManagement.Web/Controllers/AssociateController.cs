using AssociateManagement.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AssociateManagement.Models {
    public class AssociateController : Controller {
        // GET: Associate
        public ActionResult Index() {
            return View();
        }

        public ActionResult GetData() {
            using (DBModel dbModel = new DBModel()) {
                List<EmployeeRecord> employees = dbModel.EmployeeRecords.ToList<EmployeeRecord>();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0) {

            return View(new EmployeeRecord());
        }

        [HttpPost]
        public ActionResult AddOrEdit(EmployeeRecord emp) {
            using (DBModel dbModel = new DBModel()) {
                dbModel.EmployeeRecords.Add(emp);
                dbModel.SaveChanges();
                return Json(new { success = true, message = "Record Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}