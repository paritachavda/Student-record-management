using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace SRM
{
    public partial class Manage_Course : System.Web.UI.Page
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
                    cmd.CommandText = "select * from Course ";

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
                cmd.CommandText = "Insert into Course(C_Id,C_name,Qualification,No_of_year) values(@C_Id,@C_name,@Qualification,@No_of_year)";
                cmd.Parameters.AddWithValue("@C_Id", TextBox1.Text);
                cmd.Parameters.AddWithValue("@C_name", TextBox2.Text);
                cmd.Parameters.AddWithValue("@Qualification", TextBox3.Text);
                cmd.Parameters.AddWithValue("@No_of_Year", Int32.Parse(TextBox4.Text));
                cmd.Connection = con;
                con.Open();
                int a = cmd.ExecuteNonQuery();
                Label1.Text = "Data added Successfully.";



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
                    cmd.CommandText = "Select C_id,C_name,Qualification,No_of_year From Course where S_id=@sid";
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

    }



}
        
    


        
    
