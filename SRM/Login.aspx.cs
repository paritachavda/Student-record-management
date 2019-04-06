using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace SRM
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            string connStr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connStr;
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from User1 where UserId =@UserId and Password=@Password";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@UserId", txtUserName.Text);
            cmd.Parameters.AddWithValue("@Password", txtPwd.Text);

            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Session["UserId"] = dt.Rows[0]["UserId"].ToString();
                    if (dt.Rows[0]["UserId"].ToString() == "Admin")
                    {
                        
                            Response.Redirect("HomePage_Admin.aspx");
                    }
                    else
                    {
                        Response.Redirect("Home.aspx");
                    }

                }
                else
                {
                    lblStatus.Text = "Invalid username or password. Please try again.";
                }
            }



            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
            }

        }
    }
}
            