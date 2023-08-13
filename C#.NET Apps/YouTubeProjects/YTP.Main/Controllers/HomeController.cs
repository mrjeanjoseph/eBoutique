using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTP.Main.Models;

namespace YTP.Main.Controllers
{
    public class HomeController : Controller
    {
        readonly DbAccess dbop = new DbAccess();
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
    }
}