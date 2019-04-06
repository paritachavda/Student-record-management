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
namespace SRM
{
    public partial class Manage_Faculty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            string constr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
            con.ConnectionString = constr;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Insert into Faculty(F_Id,F_name,L_name,Contact_Info,Qualification) values(@F_id,@F_name,@L_name,@Contact_Info,@Qualification)";
                cmd.Parameters.AddWithValue("@F_id", TextBox1.Text);
                cmd.Parameters.AddWithValue("@F_name", TextBox2.Text);
                cmd.Parameters.AddWithValue("@L_name", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Contact_Info", TextBox4.Text);
                cmd.Parameters.AddWithValue("@Qualification", TextBox5.Text);
                cmd.Connection = con;
                int a = cmd.ExecuteNonQuery();
                Label1.Text="Data added successfully.";
            }
           catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    Label1.Visible = true;
                    Label1.Text = "This data alredy exist";
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

        }
    }
}