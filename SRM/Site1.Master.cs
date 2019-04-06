using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SRM
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*try
            {
                MembershipUser membershipUser = Membership.GetUser();
                if (membershipUser != null)
                {
                    string loggedinuser = Membership.GetUser().ToString();
                    Label1.Text = "Welcome " + loggedinuser;
                }
                else
                {
                    Label1.Text = "";
                }

            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }*/
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("Login.aspx");

        }

       
    }
}


