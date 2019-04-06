using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

namespace SRM
{
    public partial class Display_Sub : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            string sid = Convert.ToString(Session["UserId"]);
            // Label1.Text = sid;
            try
            {
                string connStr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                con.ConnectionString = connStr;
                cmd.CommandText = "Sub_Code,Sub_Name,Sem,Credit from Subject where Sub_Code =@Sub_Code";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Sub_Code",sid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {

                    string Sub_Code = dt.Rows[0]["Sub_Code"].ToString();
                    string Sub_Name = dt.Rows[0]["Sub_Name"].ToString();
                    // Label1.Text = sem;
                    cmd.CommandText = "Select Sub_Name from Subject where Sub_Code=@Sub_Code";
                    cmd.Connection = con;
                    DataTable dt1 = new DataTable();
                    cmd.Parameters.AddWithValue("@Sub_Code",Sub_Code);
                    cmd.Parameters.AddWithValue("@Sub_Name",Sub_Name);
                    con.Open();
                    da.Fill(dt1);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt1;
                        GridView1.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GridView1.SelectedIndex;
            string sub = GridView1.Rows[index].Cells[1].Text;

            try
            {

                string connStr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = connStr;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select Sub_Code from Subject where Sub_Name=@subnm";

                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@subnm", sub);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {

                    string subcode = dt.Rows[0]["Sub_Code"].ToString();
                    cmd.CommandText = "select F_name,L_name,Contact_Info,Qualification from Faculty where  Sub_Code=@subcode";

                    DataTable dt1 = new DataTable();
                    cmd.Parameters.AddWithValue("@subcode", subcode);
                    con.Open();
                    da.Fill(dt1);
                    con.Close();

                    if (dt1.Rows.Count > 0)
                    {
                        TextBox2.Text = dt1.Rows[0]["F_name"].ToString();
                        TextBox3.Text = dt1.Rows[0]["L_name"].ToString();
                        TextBox4.Text = dt1.Rows[0]["Contact_Info"].ToString();
                        TextBox5.Text = dt1.Rows[0]["Qualification"].ToString();
                    }
                    else
                        Label1.Text = "No Subject";
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
        }
    }
}
          