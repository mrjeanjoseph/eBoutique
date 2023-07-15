using RecordKeeping.Projects.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RecordKeeping.Projects.DataAccess {
    public class DataAccessLayer {
        public string InsertData(Customer objcust) {
            SqlConnection con = null;
            string result = "";

            try {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConn"].ToString());
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
    }
}