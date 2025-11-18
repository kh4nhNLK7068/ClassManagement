using Dapper;
using System;
using System.Data.SqlClient;

public partial class ChangePasswordForm : System.Web.UI.Page 
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ChangePassword(object sender, EventArgs e)
    {
        using (var connection = new SqlConnection(conn))
        {
            string username = (String)Session["Username"];
            var sql = "SELECT u.[Password] FROM [User] u WHERE u.Username = @Username;";
            var currentPassword = connection.QueryFirstOrDefault<string>(sql, new { Username = username });

            if (currentPass.Text.Length == 0 || newPass.Text.Length == 0 || confirmPass.Text.Length == 0)
            {
                Notif.Text = "Fields cannot be left blank";
                Notif.Show();
                return;
            }
            if (currentPass.Text != currentPassword)
            {
                Notif.Text = "Please enter correct current password";
                Notif.Show();
                return;
            }
            if(newPass.Text != confirmPass.Text)
            {
                Notif.Text = "Confirmation password does not match";
                Notif.Show();
                return;
            }

            connection.Execute("UPDATE [User] SET [Password] = @Password WHERE [Username] = @Username;",
                new { Password = newPass.Text, Username = username });
            Notif.Text = "Changed password successfully!";
            Notif.Show();
        }
    }
}
