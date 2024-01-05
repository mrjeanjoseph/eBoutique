using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Students : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        if (Session["SID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            con = new SqlConnection();
            cmd = new SqlCommand();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.CommandText = "SELECT fullname FROM Students WHERE id = " + Session["SID"];
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Page.Title = reader[0].ToString();
                reader.Close();
            }
            else
                reader.Close();
            con.Close();
        }
    }

    protected void LinkButton_Logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("Default.aspx");
    }
}