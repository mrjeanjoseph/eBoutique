using AssociateManagement.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

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

            if(id == 0) 
                return View(new EmployeeRecord());
            else {
                using (DBModel db = new DBModel()) {
                    var result = db.EmployeeRecords
                        .Where(rec => rec.EmployeeID == id)
                        .FirstOrDefault<EmployeeRecord>();

                    return View(result);
                }
            }            
        }

        [HttpPost]
        public ActionResult AddOrEdit(EmployeeRecord emp) {

            using (DBModel db = new DBModel()) {

                if(emp.EmployeeID == 0) {
                    db.EmployeeRecords.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Record Saved Successfully" }, JsonRequestBehavior.AllowGet);
                } else {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Record Updated Successfully" }, JsonRequestBehavior.AllowGet);

                }
            }
        }

        [HttpPost] public ActionResult Delete(int id) {

            using (DBModel db = new DBModel()) {

                EmployeeRecord emp = db.EmployeeRecords
                    .Where (rec => rec.EmployeeID == id)
                    .FirstOrDefault<EmployeeRecord>();
                db.EmployeeRecords.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Record Deleted Successfully" }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}