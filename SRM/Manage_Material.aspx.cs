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
    public partial class Manage_Material : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
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
                cmd.CommandText = "Insert into Material(M_Title,M_Sub,M_Filename,M_Filepath) values(@M_Title,@M_Sub,@M_Filename,@M_Filepath)";
                cmd.Parameters.AddWithValue("@M_Title", TextBox1.Text);
                cmd.Parameters.AddWithValue("@M_Sub", TextBox2.Text);
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("Files/" + filename));
                cmd.Parameters.AddWithValue("@M_Filename", filename);
                cmd.Parameters.AddWithValue("@M_Filepath", "Files/" + filename);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                Label1.Text = "Material added successfully.";
                con.Close();
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    Label1.Visible = true;
                    Label1.Text = "This data alredy exist";
                }
                else
                {
                    Label1.Text = se.Message;
                }
            }

        }
    }
}




