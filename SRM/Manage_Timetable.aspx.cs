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
using System.IO;


namespace SRM
{
    public partial class Manage_Timetable : System.Web.UI.Page
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
                    cmd.CommandText = "select * from TimeTable ";

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

                    Label2.Text = ex.ToString();
                }

            }
        }

      

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                string constr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
                con.ConnectionString = constr;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Insert into TimeTable(T_sem,T_div,T_filename,T_filepath) values(@T_sem,@T_div,@T_filename,@T_filepath)";
                cmd.Parameters.AddWithValue("@T_Sem",DropDownList1.Text);
                cmd.Parameters.AddWithValue("@T_div", TextBox1.Text);

                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("Files/" + filename));
                cmd.Parameters.AddWithValue("@T_filename", filename);
                cmd.Parameters.AddWithValue("@T_filepath", "Files/" + filename);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                Label2.Text = "Timetable Details added successfully.";
                con.Close();
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    Label2.Visible = true;
                    Label2.Text = "This data alredy exist";
                }
                else
                {
                    Label2.Text = se.Message;
                }
            }

        }

      
    }
}
