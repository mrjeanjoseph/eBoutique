using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using YTP.Main.Areas.HaitiEmployee.Models;

namespace YTP.Main.Areas.HaitiEmployee.DataAccess {
    public class EmployeeDB {
        //declare connection string  
        readonly string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        //Return list of all Employees  
        public List<EmployeeModel> ListAll() {
            List<EmployeeModel> lst = new List<EmployeeModel>();
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                SqlCommand com = new SqlCommand("SelectEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read()) {
                    lst.Add(new EmployeeModel {
                        EmployeeID = Convert.ToInt32(rdr["EmployeeID"]),
                        EmployeeName = rdr["EmployeeName"].ToString(),
                        EmployeeAge = Convert.ToInt32(rdr["EmployeeAge"]),
                        State = rdr["State"].ToString(),
                        Country = rdr["Country"].ToString(),
                    });
                }
                return lst;
            }
        }

        //Method for Adding an Employee  
        public int Add(EmployeeModel emp) {
            int i;
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployeeId", emp.EmployeeID);
                com.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                com.Parameters.AddWithValue("@EmployeeAge", emp.EmployeeAge);
                com.Parameters.AddWithValue("@State", emp.State);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Updating Employee record  
        public int Update(EmployeeModel emp) {
            int i;
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployeeId", emp.EmployeeID);
                com.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                com.Parameters.AddWithValue("@EmployeeAge", emp.EmployeeAge);
                com.Parameters.AddWithValue("@State", emp.State);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        //Method for Deleting an Employee  
        public int Delete(int ID) {
            int i;
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployeeId", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}