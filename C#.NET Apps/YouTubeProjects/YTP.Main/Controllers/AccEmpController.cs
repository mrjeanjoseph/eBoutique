﻿using System.Linq;
using System.Web.Mvc;
using YTP.Main.Models;
using YTP.Main.ViewModel;

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

        public JsonResult AddUpdateEmployee(AccEmpData viewModel) {

            string message = "Data successfully Updated";
            if(!ModelState.IsValid) {
                var errorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();

                return Json(new {Success = false, Message = "An error occurred! We think we know why.",  ErrorList = errorList});
            }

            Acc_EmpData empObj = _dbContext.Acc_EmpData
                .SingleOrDefault(model => model.EmployeeId == viewModel.EmployeeId) ?? new Acc_EmpData();
            empObj.EmployeeId = viewModel.EmployeeId;
            empObj.FirstName = viewModel.FirstName;
            empObj.LastName = viewModel.LastName;
            empObj.Department = viewModel.Department;
            empObj.PositionType = viewModel.PositionType;
            empObj.Salary = viewModel.Salary;
            empObj.CityId = viewModel.CityId;

            if(viewModel.EmployeeId == 0) {
                message = "Data successfully added";
                _dbContext.Acc_EmpData.Add(empObj);
            }

            _dbContext.SaveChanges();
            return Json(new { Success = true, Message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}