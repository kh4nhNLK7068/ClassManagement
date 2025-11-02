using System;
using System.Configuration;
using ClassManagement.Helpers;
using ClassManagement.Models.Enums;

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
                    userRole = "IT";
                }
                if (user.StudentInfoId == null && user.TeacherInfoId != null)
                {
                    userRole = "Teacher";
                }
                else
                {
                    userRole = "Student";
                }
                string token = JwtManager.GenerateToken(user.Username, userRole);
                Session["JwtToken"] = token;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                lblResult.Text = "</br>Invalid username or password!";
            }
        }
    }
}
