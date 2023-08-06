using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YTP.Main.Models {
    public class CustomerDBHandle {

        private SqlConnection con;
        private void Connection() {
            string constring = ConfigurationManager.ConnectionStrings["mycon"].ToString();
            con = new SqlConnection(constring);
        }

        // ********** VIEW STUDENT DETAILS ********************
        public List<CustomerModel> GetCustomer() {
            Connection();
            List<CustomerModel> studentlist = new List<CustomerModel>();

            SqlCommand cmd = new SqlCommand("GetCustomerDetails", con) {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows) {
                studentlist.Add(
                    new CustomerModel {
                        CustomerId = Convert.ToInt32(dr["CustomerId"]),
                        CompanyName = Convert.ToString(dr["CompanyName"]),
                        ContactName = Convert.ToString(dr["ContactName"]),
                        ContactTitle = Convert.ToString(dr["ContactTitle"]),
                        Country = Convert.ToString(dr["Country"])
                    });
            }
            return studentlist;
        }

    }
}