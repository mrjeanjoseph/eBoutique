using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Lecturers : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader reader;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LID"] == null)
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
            cmd.CommandText = "SELECT fullname FROM Lectures WHERE id = " + Session["LID"];
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

    protected void GV_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField HFID = (HiddenField)e.Row.FindControl("hf_iD");
            HyperLink HlM = (HyperLink)e.Row.FindControl("HyperLink_M");
            Session["MID"] = HFID.Value;
            HlM.NavigateUrl = "Degrees.aspx?id=" + HFID.Value;
        }
    }

    protected void LinkButton_Logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("Default.aspx");
    }
}