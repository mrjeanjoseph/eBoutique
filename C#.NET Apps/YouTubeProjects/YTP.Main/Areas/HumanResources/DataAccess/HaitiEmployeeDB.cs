using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using YTP.Main.Areas.HumanResources.Models;

namespace YTP.Main.Areas.HumanResources.DataAccess {

    public class HaitiEmployeeDB {
        //declare connection string  
        readonly string cs = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;

        //Return list of all Employees  
        public List<HaitiEmployeeModel> ListAll() {
            List<HaitiEmployeeModel> lst = new List<HaitiEmployeeModel>();
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                SqlCommand com = new SqlCommand("SP_Select_Haiti_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read()) {
                    lst.Add(new HaitiEmployeeModel {
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
        public int Add(HaitiEmployeeModel emp) {
            int i;
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                SqlCommand com = new SqlCommand("SP_InsertUpdate_Haiti_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
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
        public int Update(HaitiEmployeeModel emp) {
            int i;
            using (SqlConnection con = new SqlConnection(cs)) {
                con.Open();
                SqlCommand com = new SqlCommand("SP_InsertUpdate_Haiti_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
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
                SqlCommand com = new SqlCommand("SP_Delete_Haiti_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployeeID", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}