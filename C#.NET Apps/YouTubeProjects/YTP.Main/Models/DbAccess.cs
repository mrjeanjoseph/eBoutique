using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace YTP.Main.Models {
    public class DbAccess {
        SqlConnection DefaultConn = new SqlConnection("Data Source=JeanPC;Initial Catalog=DEFAULTDB;Integrated Security=True");

        // For View record
        public DataSet Empget(Employee emp, out string msg) {
            DataSet ds = new DataSet();
            msg = "";
            try {
                SqlCommand com = new SqlCommand("Sp_Employee", DefaultConn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Sr_no", emp.Sr_no);
                com.Parameters.AddWithValue("@Emp_name", emp.Emp_name);
                com.Parameters.AddWithValue("@City", emp.City);
                com.Parameters.AddWithValue("@STATE", emp.State);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Department", emp.Department);
                com.Parameters.AddWithValue("@flag", emp.flag);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds);
                msg = "OK";
                return ds;
            } catch (Exception ex) {
                msg = ex.Message;
                return ds;
            }
        }

        //For insert and update
        public string Empdml(Employee emp, out string msg) {
            msg = "";
            try {
                SqlCommand com = new SqlCommand("Sp_Employee", DefaultConn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Sr_no", emp.Sr_no);
                com.Parameters.AddWithValue("@Emp_name", emp.Emp_name);
                com.Parameters.AddWithValue("@City", emp.City);
                com.Parameters.AddWithValue("@STATE", emp.State);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Department", emp.Department);
                com.Parameters.AddWithValue("@flag", emp.flag);
                DefaultConn.Open();
                com.ExecuteNonQuery();
                DefaultConn.Close();
                msg = "OK";
                return msg;
            } catch (Exception ex) {
                if (DefaultConn.State == ConnectionState.Open) {
                    DefaultConn.Close();
                }
                msg = ex.Message;
                return msg;
            }
        }
    }
}