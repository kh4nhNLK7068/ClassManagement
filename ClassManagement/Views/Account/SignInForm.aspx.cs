using System;
using System.Web;
using ClassManagement.Helpers;

namespace ClassManagement.Views.Account
{
    public partial class SignInForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;

            var repo = new UserRepository();
            var user = repo.GetUser(username, password);

            if (user != null)
            {
                string userRole = "";
                if (user.StudentInfoId == null && user.TeacherInfoId == null)
                {
                    userRole = "Admin";
                }
                else if (user.StudentInfoId == null && user.TeacherInfoId != null)
                {
                    userRole = "Teacher";
                }
                else
                {
                    userRole = "Student";
                }
                string token = JwtManager.GenerateToken(user.Username, userRole);

                // Save token to Sesion or Cookie
                Session["JwtToken"] = token;
                Session["UserRole"] = userRole;

                // Or save it to Cookie
                HttpCookie authCookie = new HttpCookie("AuthToken", token);
                authCookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(authCookie);

                Response.Redirect("~/Views/Dashboard/Dashboard.aspx");
            }
            else
            {
                lblResult.Text = "</br>Invalid username or password!";
            }
        }
    }
}
