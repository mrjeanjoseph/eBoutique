using System;
using System.Data;
using MVC_Rehearsals.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC_Rehearsals.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Employee> employeeList = new List<Employee>();
            string CS = ConfigurationManager.ConnectionStrings["dataConn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS)) {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Employees]", con) {
                    CommandType = CommandType.Text
                };
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) {
                    var emp = new Employee {
                        EmployeeID = Convert.ToInt32(rdr["EmployeeId"]),
                        FirstName = rdr["FirstName"].ToString(),
                        LastName = rdr["LastName"].ToString(),
                        Title = rdr["Title"].ToString(),
                        BirthDate = Convert.ToDateTime(rdr["BirthDate"]),
                        HireDate = Convert.ToDateTime(rdr["HireDate"])
                    };
                    employeeList.Add(emp);
                }
                rdr.Close();
                con.Close();
            }
            return View(employeeList);
        }
    }
}