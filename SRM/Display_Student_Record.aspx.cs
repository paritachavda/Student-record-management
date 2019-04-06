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
    public partial class DisplayStudentRecord : System.Web.UI.Page
    {
         

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                string uid = Convert.ToString(Session["UserId"]);
                

                string connStr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = connStr;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Student where S_id =@UserId";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserId", uid);
                    con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);



                if (dt.Rows.Count > 0)
                {
                        Label1.Text = dt.Rows[0]["F_name"].ToString();
                        Label2.Text = dt.Rows[0]["M_name"].ToString();
                        Label3.Text = dt.Rows[0]["L_name"].ToString();
                        Label4.Text = dt.Rows[0]["Address"].ToString();
                        Label5.Text = dt.Rows[0]["Gender"].ToString();
                        Label6.Text = dt.Rows[0]["DOB"].ToString();
                        Label7.Text = dt.Rows[0]["Contact_Info"].ToString();

                    }
                
               

            }
            catch(Exception r)
            {
                Console.WriteLine(r.ToString());
            }        
           
            

    }
        }
    }