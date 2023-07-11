using EmployeeRecord.Models;
using System.Web.Mvc;

namespace EmployeeRecord.Controllers {
    public class HomeController : Controller {

        DEFAULTDB _empObject;

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmployeeModel emp) {
            _empObject = new DEFAULTDB();

            EmployeeRecord2 empData = new EmployeeRecord2 {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                PhoneNumber = emp.PhoneNumber,
                DateOfBirth = emp.DateOfBirth,
                Address = emp.Address,
                DepartmentId = emp.DepartmentId
            };

            _empObject.EmployeeRecords.Add(empData);
            _empObject.SaveChanges();

            return Json(new {SuccessMessage = "Data Added Successfully."}, JsonRequestBehavior.AllowGet);

        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}