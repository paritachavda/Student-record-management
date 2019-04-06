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
    public partial class Manage_subject : System.Web.UI.Page
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
                    cmd.CommandText = "select * from Subject ";

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
                cmd.CommandText = "Insert into Subject(Sem,Sub_Code,Sub_Name,Credit) values(@Sem,@Sub_Code,@Sub_Name,@Credit)";
                cmd.Parameters.AddWithValue("@Sem", DropDownList1.Text);
                cmd.Parameters.AddWithValue("@Sub_Code", TextBox1.Text);
                cmd.Parameters.AddWithValue("@Sub_Name", TextBox2.Text);
                cmd.Parameters.AddWithValue("@Credit", DropDownList2.Text);
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
                cmd.CommandText = ("UPDATE Subject set Sem=@Sem,Sub_Code=@Sub_Code,Sub_Name=@Sub_Name,Credit=@Credit where S_Code=@S_Code");
                con.Open();
                cmd.Parameters.AddWithValue("@Sem",DropDownList1.Text);
                cmd.Parameters.AddWithValue("@Sub_Code", TextBox1.Text);
                cmd.Parameters.AddWithValue("@Sub_Name", TextBox2.Text);
                cmd.Parameters.AddWithValue("@Credit",DropDownList2.Text);
               


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
                    DropDownList1.Text = gvr.Cells[1].Text;
                    TextBox1.Text = gvr.Cells[2].Text;
                    TextBox2.Text = gvr.Cells[3].Text;
                    DropDownList2.Text = gvr.Cells[4].Text;
                    
                    Button3.Enabled = true;
                    Button2.Enabled = true;
                    Button1.Enabled = false;
                    TextBox1.ReadOnly = true;
                    
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
                cmd.CommandText = "select (Sub_Code) from Subject where Sub_Code=@Sub_Code";
                cmd.Parameters.AddWithValue("@Sub_Code", TextBox1.Text);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {


                    string abc = rdr["Sub_Code"].ToString();
                    con.Close();
                    cmd.CommandText = "Select Sem,Sub_Code,Sub_Name,Credit from Subject where Sub_Code=@Sub_Code";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Sub_Code", abc);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    DropDownList1.Text = dt.Rows[0]["Sem"].ToString();
                    TextBox1.Text = dt.Rows[0]["Sub_Code"].ToString();
                    TextBox2.Text = dt.Rows[0]["Sub_Name"].ToString();
                    DropDownList2.Text = dt.Rows[0]["Credit"].ToString();
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
                cmd.CommandText = ("DELETE  FROM Subject  WHERE  Sub_Code=@Sub_Code");
                con.Open();
                cmd.Parameters.AddWithValue("@Sub_Code", TextBox1.Text);



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
       