using YTP.Main.Areas.HumanResources.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace YTP.Main.Areas.HumanResources.Controllers {
    public class EmployeeController : Controller
    {
        readonly Sp_EmpDBAccess dbaccessoperation = new Sp_EmpDBAccess();
        string msg;
        public ActionResult Index() {

            Employee emp = new Employee { flag = "get" };
            DataSet ds = dbaccessoperation.GetEmployeeDetail(emp, out msg);

            List<Employee> list = new List<Employee>();
            foreach (DataRow dr in ds.Tables[0].Rows) {
                list.Add(new Employee {
                    Sr_no = Convert.ToInt32(dr["Sr_no"]),
                    Emp_name = dr["Emp_name"].ToString(),
                    City = dr["City"].ToString(),
                    State = dr["State"].ToString(),
                    Country = dr["Country"].ToString(),
                    Department = dr["Department"].ToString()
                });
            }
            return View(list);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp) {

            emp = new Employee { flag = "insert" };
            try {
                emp.flag = "insert";
                dbaccessoperation.ManupilateEmployeeRecord(emp, out msg);
                TempData["msg"] = msg;
            } catch (Exception ex) {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {

            Employee emp = new Employee();
            emp.Sr_no = id;
            emp.flag = "getid";

            DataSet ds = dbaccessoperation.GetEmployeeDetail(emp, out msg);
            foreach (DataRow dr in ds.Tables[0].Rows) {
                emp.Sr_no = Convert.ToInt32(dr["Sr_no"]);
                emp.Emp_name = dr["Emp_name"].ToString();
                emp.City = dr["City"].ToString();
                emp.State = dr["State"].ToString();
                emp.Country = dr["Country"].ToString();
                emp.Department = dr["Department"].ToString();
            }
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(int id, [Bind] Employee emp) {

            try {
                emp.Sr_no = id;
                emp.flag = "update";
                dbaccessoperation.ManupilateEmployeeRecord(emp, out msg);
                TempData["msg"] = msg;
            } catch (Exception ex) {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id) {

            try {
                Employee emp = new Employee {
                    flag = "delete",
                    Sr_no = id
                };
                dbaccessoperation.ManupilateEmployeeRecord(emp, out msg);
                TempData["msg"] = msg;
            } catch (Exception ex) {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

    }
}