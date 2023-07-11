using EmployeeRecord.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EmployeeRecord.Controllers {
    public class HomeController : Controller {

        EmployeeDbContext _empDbContext;

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmployeeModel emp) {
            _empDbContext = new EmployeeDbContext();

            if (_empDbContext.Employees.Any(m => m.Code == emp.Code)) {

                //Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { responseMessage = $"Employee Code: {emp.Code} already exists." }, JsonRequestBehavior.AllowGet);
            } else {

                Employee empData = new Employee {
                    Name = emp.Name,
                    Code = emp.Code,
                    Age = emp.Age,
                    Department = emp.Department,
                    Email = emp.Email,
                    Salary = emp.Salary,
                };

                _empDbContext.Employees.Add(empData);
                _empDbContext.SaveChanges();

                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { responseMessage = "Data Added Successfully." }, JsonRequestBehavior.AllowGet);

            }


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