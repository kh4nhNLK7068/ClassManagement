using System;

namespace ClassManagement
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    lblUserName.Text = "Hello, " + Session["Username"].ToString() + "  |  ";
                }
                else
                {
                    lblUserName.Text = "Hello, xxx  |  ";
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Views/Account/SignInForm.aspx");
        }

    }
}