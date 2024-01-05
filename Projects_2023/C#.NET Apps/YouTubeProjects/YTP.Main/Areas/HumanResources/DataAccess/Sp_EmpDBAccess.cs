using YTP.Main.Areas.HumanResources.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YTP.Main.Areas.HumanResources.Controllers {
    public class Sp_EmpDBAccess {
        readonly SqlConnection DefaultConn = new SqlConnection("Data Source=JeanPC;Initial Catalog=DEFAULTDB;Integrated Security=True");

        // For View record
        public DataSet GetEmployeeDetail(Employee emp, out string msg) {
            DataSet ds = new DataSet();
            msg = "";
            try {
                SqlCommand com = new SqlCommand("Sp_Employee", DefaultConn) {
                    CommandType = CommandType.StoredProcedure
                };
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
        public string ManupilateEmployeeRecord(Employee emp, out string msg) {
            msg = "";
            try {
                SqlCommand com = new SqlCommand("Sp_Employee", DefaultConn) {
                    CommandType = CommandType.StoredProcedure
                };
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

        #region Accessing Northwind DB
        
        private SqlConnection _nwDbConn;
        private void Connection() {
            string constring = ConfigurationManager.ConnectionStrings["NorthwindDbConn"].ToString();
            _nwDbConn = new SqlConnection(constring);
        }
        #endregion
    }
}