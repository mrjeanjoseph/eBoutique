using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader reader;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminID"] == null)
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
            cmd.CommandText = "SELECT fullname FROM Admin WHERE id = " + Session["AdminID"];
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

    protected void btn_signupLec_Click(object sender, EventArgs e)
    {
        if (tb_usernameLec.Text.TrimStart().TrimEnd() == "" || tb_passLec.Text == "" || tb_fullname.Text.TrimStart().TrimEnd() == "")
        {
            lb_msg.Text = "Please enter your information. Fields cannot be blank!";
        }
        else
        {
            con.Open();
            cmd.CommandText = "SELECT id FROM Lectures WHERE username=@username AND pass=@pass";
            cmd.Parameters.AddWithValue("@username", tb_usernameLec.Text);
            cmd.Parameters.AddWithValue("@pass", tb_passLec.Text);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                lb_msg.Text = " The username and password you have selected bave been to another user, " +
                    "please choose a different username and password.\nThank you!";
            }
            else
            {
                reader.Close();
                cmd.CommandText = "INSERT INTO Lectures (username,pass,fullname,notes,active) VALUES (@username2,@pass2,@fullname,@notes,'false')";
                cmd.Parameters.AddWithValue("@username2", tb_usernameLec.Text);
                cmd.Parameters.AddWithValue("@pass2", tb_passLec.Text);
                cmd.Parameters.AddWithValue("@fullname", tb_fullname.Text);
                cmd.Parameters.AddWithValue("@notes", tb_notes.Text);
                cmd.ExecuteNonQuery();
                lb_msg.Text = "Information entered Successfully";
                GridView_Lectures.DataBind();
            }
            con.Close();
        }
    }

    protected void LinkButton_Logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("Default.aspx");
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "SELECT MName FROM Materials WHERE MName LIKE '%' + @search + '%'";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> MaterialNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader()) // BUG FOUND -
                {
                    while (sdr.Read())
                    {
                        MaterialNames.Add(sdr["MName"].ToString());
                    }
                }
                con.Close();
                return MaterialNames;
            }
        }
    }

    protected void btn_insertM_Click(object sender, EventArgs e)
    {
        if (tb_Mname.Text.TrimStart().TrimEnd() == "")
        {
            lb_msgM.Text = "Please enter Material information in the field!";
        }
        else
        {
            con.Open();
            cmd.CommandText = "SELECT Id FROM Materials WHERE MName=@MName";
            cmd.Parameters.AddWithValue("@MName", tb_Mname.Text);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                lb_msgM.Text = " The material you are trying to add has already been added, " +
                    "please add new materials!";
            }
            else
            {
                reader.Close();
                cmd.CommandText = "INSERT INTO Materials (MName,LID,Notes) VALUES (@MNameP,@LID,@MnotesP)";
                cmd.Parameters.AddWithValue("@MNameP", tb_Mname.Text);
                cmd.Parameters.AddWithValue("@LID", ddl_Lec.SelectedValue);
                cmd.Parameters.AddWithValue("@MNotesP", tb_MNotes.Text);
                cmd.ExecuteNonQuery();
                lb_msgM.Text = "Material Content added Successfully";
                GridView_Materials.DataBind();
            }
            con.Close();
        }

    }
}