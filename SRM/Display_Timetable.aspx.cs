using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.Configuration;
namespace SRM
{
    public partial class Display_Timetable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            string uid = Convert.ToString(Session["UserId"]);
            try
            {

                string connStr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = connStr;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select Current_sem from Enrolle where S_id =@UserId";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserId", uid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();


                if (dt.Rows.Count > 0)
                {
                   // Label2.Text = dt.Rows[0]["Current_sem"].ToString();
                    string sem = dt.Rows[0]["Current_sem"].ToString();
                    Label1.Text = sem;
                    DataTable dt1 = new DataTable();

                    cmd.CommandText = "select T_filename,T_filepath  from TimeTable where T_sem=sem ";
                    
                    con.Open();
                   
                    da.Fill(dt1);
                    con.Close();
                    if (dt1.Rows.Count > 0)
                    {
                        Label1.Text = dt1.Rows[0]["T_filename"].ToString();

                    }
                    else
                        Label1.Text = "No";

                    con.Close();

                }
            }
            catch (Exception r)
            {
                Console.WriteLine(r.ToString());
            }

}

protected void Button6_Click(object sender, EventArgs e)
        {
           
        }
    }
}