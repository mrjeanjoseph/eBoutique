using AssociateManagement.Web.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace AssociateManagement.Models {
    public class AssociateController : Controller {
        // GET: Associate
        public ActionResult Index() {
            return View();
        }

        public ActionResult GetData() {
            using (DBModel dbModel = new DBModel()) {
                List<EmployeeRecord> employees = new List<EmployeeRecord>();
                return Json(new {data=employees}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost] public ActionResult AddOrEdit(int id = 0) {

            return View(new EmployeeRecord());
        }

        [HttpGet] public ActionResult AddOrEdit(EmployeeRecord emp) {
            using (DBModel dbModel = new DBModel()) {
                dbModel.EmployeeRecords.Add(emp);
                dbModel.SaveChanges();
                return Json(new { success = true, message = "Record Saved successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}