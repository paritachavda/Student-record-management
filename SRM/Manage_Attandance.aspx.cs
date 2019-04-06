using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Web.Configuration;
using System.Data;




namespace SRM
{
    public partial class Manage_Attandance : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        string constr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {


                Button2.Enabled = false;
                Button3.Enabled = false;
                try
                {
                    con.ConnectionString = constr;
                    cmd.Connection = con;
                    cmd.CommandText = "select * from Enrolle ";

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();

                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                }
                catch (Exception ex)
                {

                    Label1.Text = ex.ToString();
                }

            }

        }

        

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                con.ConnectionString = constr;
                cmd.Connection = con;
                cmd.CommandText = "Insert into Enrolle(S_id,C_id,Current_sem,Division,Attendance) values(@S_id,@C_id,@Current_sem,@Division,@Attendance)";
                cmd.Parameters.AddWithValue("@S_id", TextBox1.Text);
                cmd.Parameters.AddWithValue("@C_id", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Attendance", TextBox5.Text);
                cmd.Parameters.AddWithValue("@Current_sem", TextBox2.Text);
                cmd.Parameters.AddWithValue("@Division", TextBox4.Text);
                cmd.Connection = con;
                con.Open();
                int a = cmd.ExecuteNonQuery();
                Label1.Text = "Data added successfully.";
                

                
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    Label1.Visible = true;
                    Label1.Text = "Data Already Exist";
                }
            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
           
           
            try
            {
                con.ConnectionString = constr;
                cmd.Connection = con;
                cmd.CommandText = ("UPDATE Enrolle set S_id=@S_id,C_id=@C_id,Current_sem=@sem,Division=@div,Attendance=@Attendance where S_id=@S_id");
                con.Open();
                cmd.Parameters.AddWithValue("@S_id", TextBox1.Text);
                cmd.Parameters.AddWithValue("@C_id", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Attendance", TextBox5.Text);
                cmd.Parameters.AddWithValue("@sem", TextBox2.Text);
                cmd.Parameters.AddWithValue("@div", TextBox4.Text);
                
               
                int a = cmd.ExecuteNonQuery();
                con.Close();
                Label1.Text = "Data updated successfully";
               
            }
            catch (SqlException se)
            {

                Label1.Visible = true;
                Label1.Text = se.ToString();
            }
        }


        protected void rdcheckchange(object sender, EventArgs e)
        {
            try
            {
                RadioButton btn = (RadioButton)sender;

                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                if (gvr != null)
                {
                    TextBox1.Text = gvr.Cells[1].Text;
                    TextBox3.Text = gvr.Cells[2].Text;
                    TextBox2.Text = gvr.Cells[3].Text;
                    TextBox4.Text = gvr.Cells[4].Text;
                    TextBox5.Text = gvr.Cells[5].Text;
                    Button3.Enabled = true;
                    Button2.Enabled = true;
                    Button1.Enabled = false;
                    TextBox1.ReadOnly = true;
                    TextBox3.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string constr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
            con.ConnectionString = constr;
            try
            {
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select (S_id) from Student where S_id=@S_id";
                cmd.Parameters.AddWithValue("@S_id", TextBox1.Text);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {


                    string abc = rdr["S_id"].ToString();
                    con.Close();
                    cmd.CommandText = "Select C_id,Current_sem,Division from Enrolle where S_id=@sid";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@sid", abc);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    TextBox3.Text = dt.Rows[0]["C_id"].ToString();
                    TextBox2.Text = dt.Rows[0]["Current_sem"].ToString();
                    TextBox4.Text = dt.Rows[0]["Division"].ToString();
                }


                else
                    Label1.Text = "Student with this id does not exist.";
            }

            catch (SqlException se)
            {


                Label1.Visible = true;
                Label1.Text = se.ToString();


            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            try
            {
                con.ConnectionString = constr;
                cmd.Connection = con;
                cmd.CommandText =("DELETE  FROM  Enrolle  WHERE  S_id=@S_id");
                con.Open();
                cmd.Parameters.AddWithValue("@S_id", TextBox1.Text);
               /* cmd.Parameters.AddWithValue("@C_id", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Attendance", TextBox5.Text);
                cmd.Parameters.AddWithValue("@sem", TextBox2.Text);
                cmd.Parameters.AddWithValue("@div", TextBox4.Text);*/


                int a = cmd.ExecuteNonQuery();
                con.Close();
                Label1.Text = "data Deleted successfully";

            }
            catch (SqlException se)
            {

                Label1.Visible = true;
                Label1.Text = se.ToString();
            }
        }
    }
}
       

    
    
