using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SRM
{
    public partial class HomePage_Admin : System.Web.UI.Page
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
            Response.Redirect("Manage_Student_Record.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_subject.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_Attandance.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_Timetable.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_Fee.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_HallTicket.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("Down.aspx");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_Faculty.aspx");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_Result.aspx");
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_HallTicket.aspx");
        }

      

        protected void Button13_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_Course.aspx");
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_Fee.aspx");
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            Response.Redirect("Manage_Course.aspx");
        }
    }
}