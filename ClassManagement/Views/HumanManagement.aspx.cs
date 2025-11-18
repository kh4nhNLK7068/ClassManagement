using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Telerik.Web.UI;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using ClassManagement.Models.Dtos;
using ClassManagement.Helpers;
using ClassManagement.Shared;

/* ### DOCUMENTS ###
        - User click Save
        v
        - OnItemInserting (validate here)
        v
        - RadGrid insert into DB
        v
        - OnItemInserted (show message here)
        =========  ===========  ===========
        =========  ===========  ===========

    ## Logic applied:
        ### Insert:
        1. Validate fullName not empty
        2. Check for duplicates (fullName + city)
        3. Insert TeacherInfo/StudentInfo
        4. Generate username from fullName
        5. Check for duplicate usernames → add numbers (huynhp, huynhp1, huynhp2...)
        6. Insert User with default password `*123456#`

        ### Update:
        1. Validate fullName and username
        2. Update both TeacherInfo/StudentInfo and User

        ## Helper methods:
        - IsUserExists()` - Check for duplicate users (fullName + city)
        - IsUsernameExists()` - Check for duplicate usernames
        - GenerateUsername()` - Create username from fullName (eg: "Nguyen Van An" -> "annv")
        - RemoveVietnameseTones()` - Remove Vietnamese accents

        ## Complete flow:
        User click Insert
        v
        InsertCommand (validate + check duplicates)
        v
        If OK -> Insert into DB
        v
        Rebind grid
        v
        Show message "Success! Username: xxx"
    */
[Authorize("Admin", "Teacher")]
public partial class HumanManagement : BasePage
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set default selected tab
            RadTabStrip1.SelectedIndex = 0;
            RadMultiPage1.SelectedIndex = 0;
        }
        SetTabByRole();
    }

    private void SetTabByRole()
    {
        string role = Session["UserRole"] as string;
        RadPageView2.Visible = role == "Admin";
        RadTab2.Visible = role == "Admin";
    }

    private string gridMessage = null;

    #region RadGrid1 - Students Tab
    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("Loading RadGrid1 - Students");
            using (var connection = new SqlConnection(conn))
            {
                var sql = @"SELECT t.ID, 
                                t.FullName, 
                                t.DoB AS DateOfBirth, 
                                t.CityLive, 
                                CAST(t.[Status] AS bit) AS Active, 
                                u.[Username] 
                            FROM StudentInfo t 
                            JOIN [User] u ON u.StudentInfoId = t.ID;";
                var data = connection.Query<UserInfoDto>(sql).ToList();
                RadGrid1.DataSource = data;
            }
        }
        catch (Exception ex)
        {
            // Log error
            System.Diagnostics.Debug.WriteLine($"Error loading students: {ex.Message}");
            RadGrid1.DataSource = new DataTable();
        }
    }

    protected void RadGrid1_DataBound(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(gridMessage))
        {
            Notif.Text = gridMessage;
            Notif.Show();
        }
    }

    protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            var item = (GridEditableItem)e.Item;

            // Get data from form
            TextBox txtFullName = item.FindControl("txtFullName") as TextBox;
            string fullName = txtFullName?.Text.Trim();

            var dp = item.FindControl("dpDateOfBirth") as RadDatePicker;
            DateTime? dob = dp?.SelectedDate;

            TextBox txtCity = item.FindControl("txtCity") as TextBox;
            string city = txtCity?.Text.Trim();

            // Validate
            if (string.IsNullOrEmpty(fullName))
            {
                e.Canceled = true;
                Notif.Text = "Full name is required!";

                return;
            }

            if (IsUserExists(fullName, city, "Insert"))
            {
                e.Canceled = true;
                Notif.Text = "User already exists with this name and city!";
                Notif.Show();
                return;
            }
            // Generate username and insert User
            string username = GenerateUsername(fullName);

            // Insert into DB
            using (var connection = new SqlConnection(conn))
            {
                // Insert StudentInfo
                int infoId = connection.ExecuteScalar<int>(@"
                    INSERT INTO StudentInfo (FullName, DoB, CityLive, Status)
                    VALUES (@FullName, @DoB, @CityLive, @Status);
                    SELECT CAST(SCOPE_IDENTITY() as int)",
                    new { FullName = fullName, DoB = dob, CityLive = city, Status = 1 });

                // Check username
                if (IsUsernameExists(username))
                {
                    // Add number at the end if duplicate
                    int counter = 1;
                    string originalUsername = username;
                    while (IsUsernameExists(username))
                    {
                        username = originalUsername + counter;
                        counter++;
                    }
                }

                string password = "*123456#";
                connection.Execute(@"
                    INSERT INTO [User] (Username, Password, Status, StudentInfoId)
                    VALUES (@Username, @Password, 1, @InfoId)",
                    new { Username = username, Password = password, InfoId = infoId });
            }

            // Rebind grid
            RadGrid1.Rebind();
            Notif.Text = "Student inserted successfully! Username: " + username;
            Notif.Show();
        }
        catch (Exception ex)
        {
            e.Canceled = true;
            Notif.Text = "Insert failed: " + ex.Message;
            Notif.Show();
            System.Diagnostics.Debug.WriteLine($"Error inserting ttudent: {ex.Message}");
        }
    }
    protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            var item = (GridEditableItem)e.Item;
            int studentId = Convert.ToInt32(item.GetDataKeyValue("ID"));

            // Get data form form
            TextBox txtFullName = item.FindControl("txtFullName") as TextBox;
            string fullName = txtFullName?.Text.Trim();

            var dp = item.FindControl("dpDateOfBirth") as RadDatePicker;
            DateTime? dob = dp?.SelectedDate;

            TextBox txtCity = item.FindControl("txtCity") as TextBox;
            string city = txtCity?.Text.Trim();

            // Validate
            if (string.IsNullOrEmpty(fullName))
            {
                e.Canceled = true;
                Notif.Text = "Full name is required!";
                Notif.Show();
                return;
            }

            if (IsUserExists(fullName, city, "Update", studentId))
            {
                e.Canceled = true;
                Notif.Text = "User already exists with this name and city!";
                Notif.Show();
                return;
            }

            // Update DB
            using (var connection = new SqlConnection(conn))
            {
                // Update StudentInfo
                connection.Execute(@"
                    UPDATE StudentInfo 
                    SET FullName = @FullName, DoB = @DoB, CityLive = @CityLive
                    WHERE ID = @ID",
                    new { FullName = fullName, DoB = dob, CityLive = city, ID = studentId });

                // Generate and Update new username 
                string newUsername = GenerateUsername(fullName);
                string oldUsername = connection.QuerySingleOrDefault<string>(@"SELECT [Username] FROM [User] WHERE StudentInfoId = @StudentId;", new { StudentId = studentId }).ToString();
                string originalUsername = newUsername;

                // Check Username 
                if (IsUsernameExists(newUsername))
                {
                    // Add number at the end if duplicate
                    int counter = 1;
                    while (IsUsernameExists(newUsername))
                    {
                        newUsername = originalUsername + counter;
                        counter++;
                    }
                }
                // Ex: annv1, annv2, annv6 (update info) -> still hold annv6 after updating infor
                if (newUsername.Contains(originalUsername)
                    && oldUsername.Contains(originalUsername))
                { }
                else
                {
                    connection.Execute(@"
                        UPDATE [User] 
                        SET [Username] = @Username
                        WHERE StudentInfoId = @ID",
                    new { Username = newUsername, ID = studentId });
                }
            }

            RadGrid1.Rebind();
            Notif.Text = "Student updated successfully!";
            Notif.Show();
        }
        catch (Exception ex)
        {
            e.Canceled = true;
            Notif.Text = "Update failed: " + ex.Message;
            Notif.Show();
            System.Diagnostics.Debug.WriteLine($"Error updating student: {ex.Message}");
        }
    }
    #endregion


    #region RadGrid2 - Teachers Tab
    protected void RadGrid2_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("Loading RadGrid2 - Teachers");

            using (var connection = new SqlConnection(conn))
            {
                var sql = @"SELECT t.ID,
                                t.FullName, 
                                t.DoB AS DateOfBirth, 
                                t.CityLive, 
                                CAST(t.[Status] AS bit) AS Active, 
                                u.[Username] 
                            FROM TeacherInfo t 
                            JOIN [User] u ON u.TeacherInfoId = t.ID;";
                var data = connection.Query<UserInfoDto>(sql).ToList();
                RadGrid2.DataSource = data;
            }
        }
        catch (Exception ex)
        {
            // Log error
            System.Diagnostics.Debug.WriteLine($"Error loading teachers: {ex.Message}");
            RadGrid2.DataSource = new DataTable();
        }
    }

    protected void RadGrid2_DataBound(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(gridMessage))
        {
            Notif.Text = gridMessage;
            Notif.Show();
        }
    }

    protected void RadGrid2_InsertCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            var item = (GridEditableItem)e.Item;

            // Get data from form
            TextBox txtFullName = item.FindControl("txtFullName") as TextBox;
            string fullName = txtFullName?.Text.Trim();

            var dp = item.FindControl("dpDateOfBirth") as RadDatePicker;
            DateTime? dob = dp?.SelectedDate;

            TextBox txtCity = item.FindControl("txtCity") as TextBox;
            string city = txtCity?.Text.Trim();

            // Validate
            if (string.IsNullOrEmpty(fullName))
            {
                e.Canceled = true;
                Notif.Text = "Full name is required!";
                Notif.Show();
                return;
            }

            if (IsUserExists(fullName, city, "Insert"))
            {
                e.Canceled = true;
                Notif.Text = "User already exists with this name and city!";
                Notif.Show();
                return;
            }

            // Generate username and insert User
            string username = GenerateUsername(fullName);

            // Insert into DB
            using (var connection = new SqlConnection(conn))
            {
                // Insert TeacherInfo
                int infoId = connection.ExecuteScalar<int>(@"
                    INSERT INTO TeacherInfo (FullName, DoB, CityLive, Status)
                    VALUES (@FullName, @DoB, @CityLive, @Status);
                    SELECT CAST(SCOPE_IDENTITY() as int)",
                    new { FullName = fullName, DoB = dob, CityLive = city, Status = 1 });

                // Check username
                if (IsUsernameExists(username))
                {
                    // Add number at the end if duplicate
                    int counter = 1;
                    string originalUsername = username;
                    while (IsUsernameExists(username))
                    {
                        username = originalUsername + counter;
                        counter++;
                    }
                }

                string password = "*123456#";
                connection.Execute(@"
                    INSERT INTO [User] (Username, Password, Status, TeacherInfoId)
                    VALUES (@Username, @Password, 1, @InfoId)",
                    new { Username = username, Password = password, InfoId = infoId });
            }

            // Rebind grid
            RadGrid2.Rebind();
            Notif.Text = "Teacher inserted successfully! Username: " + username;
            Notif.Show();
        }
        catch (Exception ex)
        {
            e.Canceled = true;
            Notif.Text = "Insert failed: " + ex.Message;
            Notif.Show();
            System.Diagnostics.Debug.WriteLine($"Error inserting teacher: {ex.Message}");
        }
    }

    protected void RadGrid2_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            var item = (GridEditableItem)e.Item;
            int teacherId = Convert.ToInt32(item.GetDataKeyValue("ID"));

            // Get data form form
            TextBox txtFullName = item.FindControl("txtFullName") as TextBox;
            string fullName = txtFullName?.Text.Trim();

            var dp = item.FindControl("dpDateOfBirth") as RadDatePicker;
            DateTime? dob = dp?.SelectedDate;

            TextBox txtCity = item.FindControl("txtCity") as TextBox;
            string city = txtCity?.Text.Trim();

            // Validate
            if (string.IsNullOrEmpty(fullName))
            {
                e.Canceled = true;
                Notif.Text = "Full name is required!";
                Notif.Show();
                return;
            }

            if (IsUserExists(fullName, city, "Update", teacherId))
            {
                e.Canceled = true;
                Notif.Text = "User already exists with this name and city!";
                Notif.Show();
                return;
            }

            // Update DB
            using (var connection = new SqlConnection(conn))
            {
                // Update TeacherInfo
                connection.Execute(@"
                    UPDATE TeacherInfo 
                    SET FullName = @FullName, DoB = @DoB, CityLive = @CityLive
                    WHERE ID = @ID",
                    new { FullName = fullName, DoB = dob, CityLive = city, ID = teacherId });

                // Generate and Update new username 
                string newUsername = GenerateUsername(fullName);
                string oldUsername = connection.QuerySingleOrDefault<string>(@"SELECT [Username] FROM [User] WHERE TeacherInfoId = @TeacherId;", new { TeacherId = teacherId }).ToString();
                string originalUsername = newUsername;

                // Check Username 
                if (IsUsernameExists(newUsername))
                {
                    // Add number at the end if duplicate
                    int counter = 1;
                    while (IsUsernameExists(newUsername))
                    {
                        newUsername = originalUsername + counter;
                        counter++;
                    }
                }
                // Ex: annv1, annv2, annv6 (update info) -> still hold annv6 after updating infor
                if (newUsername.Contains(originalUsername)
                    && oldUsername.Contains(originalUsername))
                { }
                else
                {
                    connection.Execute(@"
                        UPDATE [User] 
                        SET [Username] = @Username
                        WHERE TeacherInfoId = @ID",
                    new { Username = newUsername, ID = teacherId });
                }
            }

            RadGrid2.Rebind();
            Notif.Text = "Teacher updated successfully!";
            Notif.Show();
        }
        catch (Exception ex)
        {
            e.Canceled = true;
            Notif.Text = "Update failed: " + ex.Message;
            Notif.Show();
            System.Diagnostics.Debug.WriteLine($"Error updating teacher: {ex.Message}");
        }
    }
    #endregion


    #region Switch Status Handler
    protected void switchStatus_CheckedChanged(object sender, EventArgs e)
    {
        var switchControl = (RadSwitch)sender;
        var item = (GridDataItem)switchControl.NamingContainer;
        var userInfoId = Convert.ToInt32(item.GetDataKeyValue("ID"));
        var newStatus = (bool)switchControl.Checked ? 1 : 0;

        try
        {
            using (var connection = new SqlConnection(conn))
            {
                if (userInfoId > -1)
                {
                    string updateSql;
                    // Index teacher page - 1 and student page - 0
                    if (RadTabStrip1.SelectedIndex == 1)
                    {
                        //TeacherInfo table
                        updateSql = "UPDATE TeacherInfo SET [Status] = @Status WHERE ID = @Id;";
                        connection.Execute(updateSql, new { Status = newStatus, Id = userInfoId });
                        //User table
                        updateSql = "UPDATE [User] SET [Status] = @Status WHERE TeacherInfoId = @TeacherInfoId;";
                        connection.Execute(updateSql, new { Status = newStatus, TeacherInfoId = userInfoId });
                    }
                    else
                    {
                        updateSql = "UPDATE StudentInfo SET [Status] = @Status WHERE ID = @Id;";
                        connection.Execute(updateSql, new { Status = newStatus, Id = userInfoId });

                        updateSql = "UPDATE [User] SET [Status] = @Status WHERE StudentInfoId = @StudentInfoId;";
                        connection.Execute(updateSql, new { Status = newStatus, StudentInfoId = userInfoId });

                        //Remove student when their status is 'Inactive' - value 0
                        if(newStatus == 0)
                        {
                            var removeStudentInClassSql = @"DELETE sc
                                                            FROM StudentInClass AS sc
                                                            JOIN [Class] AS c ON sc.ClassId = c.ID 
                                                            WHERE sc.StudentId = @StudentId AND c.Status = 'In-process';";
                            connection.Execute(removeStudentInClassSql, new { StudentId = userInfoId });
                        }
                    }

                    // Refresh grid
                    if (item.OwnerTableView.ParentItem == null) // Grid1 - Teachers
                    {
                        RadGrid1.Rebind();
                    }
                    else // Grid2 - Students
                    {
                        RadGrid2.Rebind();
                    }
                    Notif.Text = $"Status updated to {(newStatus == 1 ? "Active" : "Inactive")} successfully!";
                    Notif.Show();
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating status: {ex.Message}");
            Notif.Text = "Error updating status: " + ex.Message;
            Notif.Show();
        }
    }
    #endregion

    #region Helpers
    private bool IsUserExists(string fullName, string city, string mode, int userId = 0)
    {
        using (var connection = new SqlConnection(conn))
        {
            int id = userId;
            string[] tables = { "TeacherInfo", "StudentInfo" };
            var sql = "";
            var count = 0;
            foreach (var table in tables)
            {
                if(mode == "Update") // Exclude current user
                {
                    sql = $"SELECT COUNT(*) FROM {table} WHERE FullName = @FullName AND CityLive = @City AND ID != @Id";
                    count += connection.ExecuteScalar<int>(sql, new { FullName = fullName, City = city, Id = userId });
                }
                else
                {
                    sql = $"SELECT COUNT(*) FROM {table} WHERE FullName = @FullName AND CityLive = @City";
                    count += connection.ExecuteScalar<int>(sql, new { FullName = fullName, City = city });
                }
            }
            return count > 0;
        }
    }

    private bool IsUsernameExists(string username)
    {
        using (var connection = new SqlConnection(conn))
        {

            var sql = "SELECT COUNT(*) FROM [User] WHERE Username = @Username;";
            var count = connection.ExecuteScalar<int>(sql, new { Username = username });
            return count > 0;
        }
    }

    private string GenerateUsername(string fullName)
    {
        // Ex: "Nguyen Le Khanh" => "khanhnl"
        var parts = fullName.Trim().Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return "user";

        string lastName = parts[parts.Length - 1].ToLower();
        string initials = "";
        for (int i = 0; i < parts.Length - 1; i++)
        {
            initials += parts[i][0];
        }

        lastName = RemoveVietnameseTones(lastName);

        return $"{lastName}{initials.ToLower()}";
    }

    private string RemoveVietnameseTones(string text)
    {
        string[] vietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        for (int i = 1; i < vietnameseSigns.Length; i++)
        {
            for (int j = 0; j < vietnameseSigns[i].Length; j++)
            {
                text = text.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
            }
        }

        return text;
    }
    #endregion
}
