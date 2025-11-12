using System;
using System.Web;

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

                SetMenuByRole();
            }
        }

        private void SetMenuByRole()
        {
            string role = Session["UserRole"] as string; //Debug here
            MyClassSite.Visible = role == "Student";
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            // Delete Cookie
            if(Request.Cookies["AuthToken"] != null)
            {
                HttpCookie cookie = new HttpCookie("AuthToken");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            Response.Redirect("~/Views/Account/SignInForm.aspx");
        }
    }
}