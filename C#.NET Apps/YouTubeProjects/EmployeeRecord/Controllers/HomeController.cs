using EmployeeRecord.Models;
using System.Web.Mvc;

namespace EmployeeRecord.Controllers {
    public class HomeController : Controller {

        EmployeeDbContext _empObject;

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmployeeModel emp) {
            _empObject = new EmployeeDbContext();

            Employee empData = new Employee {
                Name = emp.Name,
                Code = emp.Code,
                Age = emp.Age,
                Department = emp.Department,
                Email = emp.Email,
                Salary = emp.Salary,
            };

            _empObject.Employees.Add(empData);
            _empObject.SaveChanges();

            return Json(new { SuccessMessage = "Data Added Successfully." }, JsonRequestBehavior.AllowGet);

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