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
    public partial class ViewHallTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                cmd.CommandText = "select * from HallTicket";


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
            cmd.CommandText = "";
            cmd.CommandText = "select Name,type,data from  HallTicket where S_id=@S_id and Sem=(Select Current_sem from Enrolle where S_id =@UserId)";
            cmd.Connection = con;
            
            cmd.Parameters.AddWithValue("S_id", Session["UserId"]);
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

        
    
