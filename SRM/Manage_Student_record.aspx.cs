using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Web.Configuration;

namespace SRM
{
    public partial class Manage_Student_record : System.Web.UI.Page
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
                    cmd.CommandText = "select * from Student";
                    

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
                cmd.CommandText = "Insert into Student(S_id,F_name,M_name,L_name,Address,Gender,DOB,Contact_Info) values(@S_id,@F_name,@M_name,@L_name,@Address,@Gender,@DOB,@Contact_Info)";
                
                cmd.Parameters.AddWithValue("@S_id", TextBox9.Text);
                cmd.Parameters.AddWithValue("@F_name", TextBox13.Text);
                cmd.Parameters.AddWithValue("@M_name", TextBox14.Text);
                cmd.Parameters.AddWithValue("@L_name", TextBox15.Text);
                cmd.Parameters.AddWithValue("@Address", TextBox16.Text);
                cmd.Parameters.AddWithValue("@Gender", DropDownList3.Text);
                cmd.Parameters.AddWithValue("@DOB", DateTime.Parse(TextBox17.Text));
                cmd.Parameters.AddWithValue("@Contact_Info", TextBox18.Text);
                
                


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
                cmd.CommandText = ("UPDATE Student set S_id=@S_id,F_name=@F_name,M_name=@M_name,L_name=@L_name,Address=@Address,Gender=@Gender,DOB=@DOB,Contact_Info=@Contact_Info where S_id=@S_id");
                con.Open();
                cmd.Parameters.AddWithValue("@S_id", TextBox9.Text);
                cmd.Parameters.AddWithValue("@F_name", TextBox13.Text);
                cmd.Parameters.AddWithValue("@M_name", TextBox14.Text);
                cmd.Parameters.AddWithValue("@L_name", TextBox15.Text);
                cmd.Parameters.AddWithValue("@Address", TextBox16.Text);
                cmd.Parameters.AddWithValue("@Gender", DropDownList3.Text);
                cmd.Parameters.AddWithValue("@DOB", DateTime.Parse(TextBox17.Text));
                cmd.Parameters.AddWithValue("@Contact_Info", TextBox18.Text);
                

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
                    TextBox9.Text = gvr.Cells[1].Text;
                    TextBox13.Text = gvr.Cells[2].Text;
                    TextBox14.Text = gvr.Cells[3].Text;
                    TextBox15.Text = gvr.Cells[4].Text;
                    TextBox16.Text = gvr.Cells[5].Text;
                    DropDownList3.Text = gvr.Cells[6].Text;
                    TextBox17.Text = gvr.Cells[7].Text;
                    TextBox18.Text = gvr.Cells[8].Text;
                    Button3.Enabled = true;
                    Button2.Enabled = true;
                    Button1.Enabled = false;
                    TextBox9.ReadOnly = true;
                   
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
                cmd.Parameters.AddWithValue("@S_id", TextBox9.Text);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {


                    string abc = rdr["S_id"].ToString();
                    con.Close();
                    cmd.CommandText = "Select S_id,F_name,M_name,L_name,Address,Gender,DOB,Contact_Info from Student where S_id=@sid";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@sid", abc);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    TextBox9.Text = dt.Rows[0]["S_id"].ToString();
                    TextBox13.Text = dt.Rows[0]["F_name"].ToString();
                    TextBox14.Text = dt.Rows[0]["M_name"].ToString();
                    TextBox15.Text = dt.Rows[0]["L_name"].ToString();
                    TextBox16.Text = dt.Rows[0]["Address"].ToString();
                    DropDownList3.Text = dt.Rows[0]["Gender"].ToString();
                    TextBox17.Text = dt.Rows[0]["DOB"].ToString();
                    TextBox18.Text = dt.Rows[0]["Contact_Info"].ToString();
                    

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
                cmd.CommandText = ("DELETE  FROM  Student  WHERE  S_id=@S_id");
                con.Open();
                cmd.Parameters.AddWithValue("@S_id", TextBox9.Text);
               


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
