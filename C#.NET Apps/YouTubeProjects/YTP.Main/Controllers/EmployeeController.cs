using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers {
    public class EmployeeController : Controller
    {

        db dbop = new db();
        string msg;

        public ActionResult Index() {
            Employee emp = new Employee();
            emp.flag = "get";
            DataSet ds = dbop.Empget(emp, out msg);
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
        public ActionResult Create([Bind] Employee emp) {

            try {
                emp.flag = "insert";
                dbop.Empdml(emp, out msg);
                TempData["msg"] = msg;

            } catch (Exception ex) {
                TempData["msg"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}