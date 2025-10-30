using System;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace ClassManagement.Views.Account
{
    public partial class SignUpForm : System.Web.UI.Page
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            DateTime dob = txtDateOfBirth.SelectedDate ?? DateTime.MinValue;
            string city = txtCityLive.Text.Trim();

            // Xác định role
            bool isTeacher = btnTeacher.Checked;
            string role = isTeacher ? "Teacher" : "Student";

            int infoId;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                if (isTeacher)
                {
                    string sql = @"INSERT INTO TeacherInfo (FullName, DoB, CityLive, Status)
                                   VALUES (@FullName, @DoB, @CityLive, 1);
                                   SELECT CAST(SCOPE_IDENTITY() as int)";
                    infoId = connection.ExecuteScalar<int>(sql, new { FullName = fullName, DoB = dob, CityLive = city });
                }
                else
                {
                    string sql = @"INSERT INTO StudentInfo (FullName, DoB, CityLive, Status)
                                   VALUES (@FullName, @DoB, @CityLive, 1);
                                   SELECT CAST(SCOPE_IDENTITY() as int)";
                    infoId = connection.ExecuteScalar<int>(sql, new { FullName = fullName, DoB = dob, CityLive = city });
                }

                // Sinh username + password mặc định
                string username = GenerateUsername(fullName);
                string password = "*123456#";

                // Insert vào bảng User
                string insertUser = isTeacher
                    ? @"INSERT INTO [User] (Username, Password, Status, TeacherInfoId)
                        VALUES (@Username, @Password, 1, @InfoId)"
                    : @"INSERT INTO [User] (Username, Password, Status, StudentInfoId)
                        VALUES (@Username, @Password, 1, @InfoId)";

                connection.Execute(insertUser, new { Username = username, Password = password, InfoId = infoId });
            }

            lblResult.Text = "<br/>" + $" Created account: <b>{fullName}</b> ({role})<br/>" +
                             $"Username: <b>{GenerateUsername(fullName)}</b> / Password: <b>*123456#</b>";
        }

        private string GenerateUsername(string fullName)
        {
            // Ví dụ: "Nguyen Le Khanh" => "khanhnl"
            var parts = fullName.Trim().Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
            string lastName = parts[parts.Length - 1].ToLower();
            string initials = "";
            for (int i = 0; i < parts.Length - 1; i++)
            {
                initials += parts[i][0];
            }

            return $"{lastName}{initials.ToLower()}";
        }
    }
}
