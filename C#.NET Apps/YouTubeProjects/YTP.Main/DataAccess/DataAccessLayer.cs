using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using YTP.Main.Models;
using System.Collections.Generic;
using System;

namespace YTP.Main.DataAccess {
    public class DataAccessLayer {

        public CustomerModel SelectDatabyID(string CustomerID) {

            SqlConnection con = null;
            DataSet ds = null;
            CustomerModel cobj = null;

            try {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", 5);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                    cobj = new CustomerModel();
                    cobj.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"].ToString());
                    cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    cobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    cobj.Mobileno = ds.Tables[0].Rows[i]["Mobileno"].ToString();
                    cobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    cobj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());

                }

                return cobj;

            } catch {
                return cobj;

            } finally {
                con.Close();

            }
        }

        public string InsertData(CustomerModel objcust) {

            SqlConnection con = null;
            string result = "";

            try {

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", 0);
                cmd.Parameters.AddWithValue("@Name", objcust.Name);
                cmd.Parameters.AddWithValue("@Address", objcust.Address);
                cmd.Parameters.AddWithValue("@Mobileno", objcust.Mobileno);
                cmd.Parameters.AddWithValue("@Birthdate", objcust.Birthdate);
                cmd.Parameters.AddWithValue("@EmailID", objcust.EmailID);
                cmd.Parameters.AddWithValue("@Query", 1);

                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;

            } catch {
                return result = "";

            } finally {
                con.Close();
            }
        }

        public string UpdateData(CustomerModel objcust) {

            SqlConnection con = null;
            string result = "";

            try {

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", objcust.CustomerID);
                cmd.Parameters.AddWithValue("@Name", objcust.Name);
                cmd.Parameters.AddWithValue("@Address", objcust.Address);
                cmd.Parameters.AddWithValue("@Mobileno", objcust.Mobileno);
                cmd.Parameters.AddWithValue("@Birthdate", objcust.Birthdate);
                cmd.Parameters.AddWithValue("@EmailID", objcust.EmailID);

                cmd.Parameters.AddWithValue("@Query", 2);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;

            } catch {

                return result = "";

            } finally {

                con.Close();

            }

        }

        public string DeleteData(CustomerModel objcust) {

            SqlConnection con = null;
            string result = "";

            try {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerID", objcust.CustomerID);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", 3);

                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;

            } catch {

                return result = "";

            } finally {

                con.Close();

            }
        }

        public List<CustomerModel> Selectalldata() {

            SqlConnection con = null;
            DataSet ds = null;
            List<CustomerModel> custlist = null;

            try {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Customer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerID", null);
                cmd.Parameters.AddWithValue("@Name", null);
                cmd.Parameters.AddWithValue("@Address", null);
                cmd.Parameters.AddWithValue("@Mobileno", null);
                cmd.Parameters.AddWithValue("@Birthdate", null);
                cmd.Parameters.AddWithValue("@EmailID", null);
                cmd.Parameters.AddWithValue("@Query", 4);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);

                custlist = new List<CustomerModel>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {

                    CustomerModel cobj = new CustomerModel();
                    cobj.CustomerID = Convert.ToInt32(ds.Tables[0].Rows[i]["CustomerID"].ToString());
                    cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    cobj.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    cobj.Mobileno = ds.Tables[0].Rows[i]["Mobileno"].ToString();
                    cobj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    cobj.Birthdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Birthdate"].ToString());
                    custlist.Add(cobj);
                }

                return custlist;

            } catch {

                return custlist;

            } finally {

                con.Close();

            }
        }
    }
}