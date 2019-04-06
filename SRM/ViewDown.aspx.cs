using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SRM
{
    public partial class ViewDown : System.Web.UI.Page
    {

       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
            SqlConnection con = new SqlConnection();

            try
            { 
            
            
                con.ConnectionString = connStr;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from Pdf";


                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);
                con.Close();
                GridView1.DataSource = ds;
                GridView1.DataBind();
                con.Close();
               
            }
           
  catch (Exception ex)
            {

                Label1.Text = ex.ToString();
            }

        }
    

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connStr;
            SqlCommand cmd = new SqlCommand();
            
            cmd.CommandText = "select Name,type,data from  Pdf where id=@id";
            cmd.Connection = con;


           
            cmd.Parameters.AddWithValue("id", GridView1.SelectedRow.Cells[1].Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();


            if (dr.Read())
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = dr["type"].ToString();
                Response.AddHeader("content-disposition", "attachment;filename=" + dr["Name"].ToString());     // to open file prompt Box open or Save file         
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite((byte[])dr["data"]);
                Response.End();
            }

        }

    }
}
