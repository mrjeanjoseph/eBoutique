using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Degrees : System.Web.UI.Page
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
            //cmd.CommandText = "SELECT MName FROM Materials WHERE id =" + Session["MID"];
            cmd.CommandText = "SELECT MName FROM Materials WHERE Id =" + Request.QueryString["Id"];
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Page.Title = reader[0].ToString();
                reader.Close();
            }
            else
                reader.Close();

            con.Close();
            SqlDataSource_Degrees.SelectCommand = "SELECT Materials.MName, Degrees.deg, Students.fullname, " +
                "Degrees.notes FROM Degrees INNER JOIN Students ON Degrees.SID = Students.Id INNER JOIN Materials ON Degrees.MID = " +
                "Materials.Id WHERE Materials.id=" + Request.QueryString["MID"];
        }

    }

    protected void btn_insertDegree_Click(object sender, EventArgs e)
    {
        if (tb_degree.Text.TrimStart().TrimEnd() == "")
        {
            lb_msg.Text = "Fields cannot be blank!";
        }
        else
        {
            con.Open();
            cmd.CommandText = "SELECT Id FROM Materials WHERE SID=@SID AND MID=@MID";
            cmd.Parameters.AddWithValue("@SID", ddl_Sname.SelectedValue);
            cmd.Parameters.AddWithValue("@MID", Request.QueryString["Id"]);
            reader = cmd.ExecuteReader(); //DEBUG HERE (id was changed to ID)
            if (reader.Read())
            {
                reader.Close();
                lb_msg.Text = "Data cannot be duplicated. Please try again";
            }
            else
            {
                reader.Close();
                cmd.CommandText = "INSERT INTO Degrees (SID,MID,notes,deg) VALUES (@SIDP,@MIDP,@notesP,@degreeP)";
                cmd.Parameters.AddWithValue("@SIDP", ddl_Sname.SelectedValue);
                cmd.Parameters.AddWithValue("@MIDP", Request.QueryString["id"]);
                cmd.Parameters.AddWithValue("@notesP", tb_notes.Text);
                cmd.Parameters.AddWithValue("@degreeP", tb_degree.Text);
                cmd.ExecuteNonQuery();
                lb_msg.Text = "Information entered Successfully";
                SqlDataSource_Degrees.DataBind();
            }
            con.Close();
        }
    }

    protected void LinkButton_Logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("Default.aspx");
    }
}