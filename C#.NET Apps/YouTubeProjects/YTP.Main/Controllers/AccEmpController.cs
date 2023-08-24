using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class AccEmpController : Controller {

        private readonly DBContext _dbContext;
        public AccEmpController() {
            _dbContext = new DBContext();
        }
        // GET: AccEmp
        public ActionResult Index() {

            ViewBag.Cities = (from city in _dbContext.Acc_CityData
                              select new SelectListItem() {
                                  Text = city.CityName, Value = city.CityId.ToString()
                              }).ToList();

            return View();
        }

        public JsonResult GetAllEmployee() {

            var accEmpRecord = (
                from empData in _dbContext.Acc_EmpData
                join cityData in _dbContext.Acc_CityData on empData.CityId equals cityData.CityId
                select new {
                    empData.EmployeeId,
                    empData.FirstName,
                    empData.LastName,
                    empData.Department,
                    empData.PositionType,
                    empData.Salary,
                    empData.CityId,
                    cityData.CityName
                }).ToList();

            return Json(new {Success = true, data = accEmpRecord }, JsonRequestBehavior.AllowGet);
        }
    }
}