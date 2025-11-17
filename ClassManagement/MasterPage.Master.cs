using ClassManagement.Helpers;
using System;
using System.Web;

namespace ClassManagement
{
    [Authorize]
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] != null)
                {
                    lnkUserName.Text = "Hello, " + Session["Username"].ToString() + "  |  ";
                }
                else
                {
                    lnkUserName.Text = "Hello, xxx  |  ";
                }

                SetMenuByRole();
            }
        }

        private void SetMenuByRole()
        {
            string role = Session["UserRole"] as string;
            MyClassSite.Visible = role == "Student";
            CurriculumSite.Visible = (role == "Admin" || role == "Teacher");
            HumanSite.Visible = (role == "Admin" || role == "Teacher");
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