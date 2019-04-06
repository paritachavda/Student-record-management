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
    public partial class Manage_Fee : System.Web.UI.Page
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
                    cmd.CommandText = "select * from Fees ";

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
    }
}

     