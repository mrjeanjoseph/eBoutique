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
    }

}