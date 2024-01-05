using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection();
        cmd = new SqlCommand();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd.CommandText = "SELECT id FROM Admin WHERE username=@username AND pass=@pass";
        cmd.Parameters.AddWithValue("@username", tb_username.Text);
        cmd.Parameters.AddWithValue("@pass", tb_pass.Text);
        reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Session["AdminID"] = reader[0].ToString();
            reader.Close();
            Response.Redirect("Admin.aspx");
        }
        else
        {
            reader.Close();
            cmd.CommandText = "SELECT id FROM Lectures WHERE username=@usernameLec AND pass=@passLec";
            cmd.Parameters.AddWithValue("@usernameLec", tb_username.Text);
            cmd.Parameters.AddWithValue("@passLec", tb_pass.Text);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Session["LID"] = reader[0].ToString();
                reader.Close();
                Response.Redirect("Lecturers.aspx");
            }
            else
            {
                reader.Close();
                cmd.CommandText = "SELECT id FROM Students WHERE username=@usernameS AND pass=@passS";
                cmd.Parameters.AddWithValue("@usernameS", tb_username.Text);
                cmd.Parameters.AddWithValue("@passS", tb_pass.Text);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Session["SID"] = reader[0].ToString();
                    reader.Close();
                    Response.Redirect("Students.aspx");
                }
                else
                {
                    reader.Close();
                    lb_msg.Text = "Wrong username and/or password!";
                }
            }
        }
        con.Close();
    }

    protected void btn_signup_Click(object sender, EventArgs e)
    {
        if (tb_usernameS.Text.TrimStart().TrimEnd() == "" || tb_pass.Text == "" || tb_fullname.Text.TrimStart().TrimEnd() == "") // I UPDATED THE "tb_pass.Text from tb_passS.Text
        {
            lb_msg.Text = "Please enter your information. Fields cannot be blank!";
        }
        else
        {
            con.Open();
            cmd.CommandText = "SELECT id FROM Students WHERE username=@username AND pass=@pass";
            cmd.Parameters.AddWithValue("@username", tb_usernameS.Text);
            cmd.Parameters.AddWithValue("@pass", tb_pass.Text);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                lb_msg.Text = " This username and/or password are taken, please try again!";
            }
            else
            {
                reader.Close();
                cmd.CommandText = "INSERT INTO Students (username,pass,fullname,active) VALUES (@username2,@pass2,@fullname,'false')";
                cmd.Parameters.AddWithValue("@username2", tb_usernameS.Text);
                cmd.Parameters.AddWithValue("@pass2", tb_pass.Text);
                cmd.Parameters.AddWithValue("@fullname", tb_fullname.Text);
                cmd.ExecuteNonQuery();
                lb_msg.Text = "Your information has been entered Successfully";
            }
            con.Close();
        }
    }
}
