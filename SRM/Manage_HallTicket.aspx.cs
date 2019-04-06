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
    public partial class Manage_HallTicket : System.Web.UI.Page
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
            Label1.Visible = true;
            string filePath = FileUpload1.PostedFile.FileName;          // getting the file path of uploaded file
            string filename1 = Path.GetFileName(filePath);               // getting the file name of uploaded file
            string ext = Path.GetExtension(filename1);                      // getting the file extension of uploaded file
            string type = String.Empty;

            if (!FileUpload1.HasFile)
            {
                Label1.Text = "Please Select File";                          //if file uploader has no file selected
            }
            else
                   if (FileUpload1.HasFile)
            {
                try
                {

                    switch (ext)                                         // this switch code validate the files which allow to upload only PDF  file 
                    {
                        case ".pdf":
                            type = "application/pdf";
                            break;

                    }

                    if (type != String.Empty)
                    {
                        string connStr = WebConfigurationManager.ConnectionStrings["SRM"].ConnectionString;
                        SqlConnection con = new SqlConnection();
                        con.ConnectionString = connStr;
                        Stream fs = FileUpload1.PostedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);                                 //reads the   binary files
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);                           //counting the file length into bytes
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "insert into HallTicket (S_id,Sem,Name, type, data)" + " values(@S_id,@Sem,@Name, @type, @Data)";
                        cmd.Connection = con;

                        

                        cmd.Parameters.AddWithValue("@S_id", TextBox1.Text);
                        cmd.Parameters.AddWithValue("@Sem",DropDownList1.Text);

                        

                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename1;
                        cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = type;
                        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Label1.ForeColor = System.Drawing.Color.Green;
                        Label1.Text = "File Uploaded Successfully";
                    }
                    else
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Select Only PDF Files  ";                              // if file is other than speified extension 
                    }
                }
                catch (Exception ex)
                {
                    Label1.Text = "Error: " + ex.Message.ToString();
                }
            }
        }


    }
}



        

        