using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace SRM
{
    public partial class Display_Material1 : System.Web.UI.Page
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
                cmd.CommandText = "Select Current_sem,C_id from Enrolle where S_id =@UserId";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserId", sid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {

                    string sem = dt.Rows[0]["Current_sem"].ToString();
                    string cid = dt.Rows[0]["C_id"].ToString();
                    // Label1.Text = sem;
                    cmd.CommandText = "Select Sub_Name from Subject where Sem=@sem and C_id=@cid";
                    cmd.Connection = con;
                    DataTable dt1 = new DataTable();
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@cid", cid);
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

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                   
                    cmd.CommandText = "Select M_Filename,M_Filepath from Material where M_Sub=@subnm";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@subnm", sub);
                    con.Open();
                   
                   da.Fill(dt);
                    con.Close();
                if (dt.Rows.Count > 0)
                {
                    GridView2.DataSource = dt;
                    GridView2.DataBind();

                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = GridView1.SelectedIndex;
                //Session["mfilenm"] = GridView2.Rows[index].Cells[1].Text;
                string filenm = GridView2.Rows[index].Cells[1].Text;
                // Session["mfilepath"] = GridView2.Rows[index].Cells[2].Text;
                string filepath = GridView2.Rows[index].Cells[2].Text;
            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            
            

                

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string filenm = Session["mfilenm"].ToString();
            string filepath = Session["mfilepath"].ToString();
            string fp = null;
            fp = filepath;
            
            Response.ContentType = "image/jpg";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fp + "\"");
            Response.TransmitFile(Server.MapPath(fp));
            Response.End();
        }
    }
}