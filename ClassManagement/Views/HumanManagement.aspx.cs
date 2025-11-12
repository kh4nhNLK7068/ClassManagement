using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Telerik.Web.UI;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using ClassManagement.Models.Dtos;
using ClassManagement.Models.Entities;

public partial class HumanManagement : System.Web.UI.Page
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
                                u.[Username], 
                                u.[Password]
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
            DisplayMessage(gridMessage);
        }
    }

    protected void RadGrid1_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.KeepInEditMode = true;
            e.ExceptionHandled = true;
            SetMessage("Update failed. Reason: " + e.Exception.Message);
        }
        else
        {
            SetMessage("Student Information updated!");
        }
    }

    /*protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
    {
        try
        {
            var item = (GridEditableItem)e.Item;
            var fullName = (item.FindControl("FullName") as TextBox) != null
                            ? (item.FindControl("FullName") as TextBox).Text
                            : item["FullName"].Text;
            RadDatePicker dp = item.FindControl("dpDateOfBirth") as RadDatePicker;
            DateTime? dob = dp.SelectedDate;
            var city = (item["CityLive"].Controls[0] as TextBox).Text;

            if (IsUserExists(fullName, city, "Teacher"))
            {
                e.ExceptionHandled = true;
                SetMessage("User's information already exists! Please choose another.");
                //e.Canceled = true; // cancel insert
                return;
            }

            int infoId;
            using (var connection = new SqlConnection(conn))
            {
                string sql = @"INSERT INTO TeacherInfo (FullName, DoB, CityLive, Status)
                                   VALUES (@FullName, @DoB, @CityLive, 1);
                                   SELECT CAST(SCOPE_IDENTITY() as int)";
                infoId = connection.ExecuteScalar<int>(sql, new { FullName = fullName, DoB = dob, CityLive = city });

                // Generate default username + password
                string username = GenerateUsername(fullName);
                string password = "*123456#";
                string insertUser = @"INSERT INTO [User] (Username, Password, Status, TeacherInfoId)
                        VALUES (@Username, @Password, 1, @InfoId)";
                connection.Execute(insertUser, new { Username = username, Password = password, InfoId = infoId });
            }

            SetMessage("New student is inserted!");
        }
        catch (Exception ex)
        {
            e.ExceptionHandled = true;
            SetMessage("Insert failed! Reason: " + e.Exception.Message);
        }
    }*/
    protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
    {
        try
        {
            var item = (GridEditableItem)e.Item;

            string fullName = (item.FindControl("txtFullName") as TextBox).Text;
            RadDatePicker dp = item.FindControl("dpDateOfBirth") as RadDatePicker;
            DateTime? dob = dp.SelectedDate;
            string city = (item.FindControl("txtCity") as TextBox).Text;

            if (IsUserExists(fullName, city, "Teacher"))
            {
                e.ExceptionHandled = true;
                SetMessage("User's information already exists! Please choose another.");
                return;
            }

            using (var connection = new SqlConnection(conn))
            {
                int infoId = connection.ExecuteScalar<int>(@"
                INSERT INTO TeacherInfo (FullName, DoB, CityLive, Status)
                VALUES (@FullName, @DoB, @CityLive, 1);
                SELECT CAST(SCOPE_IDENTITY() as int)",
                    new { FullName = fullName, DoB = dob, CityLive = city });

                string username = GenerateUsername(fullName);
                string password = "*123456#";

                connection.Execute(@"
                INSERT INTO [User] (Username, Password, Status, TeacherInfoId)
                VALUES (@Username, @Password, 1, @InfoId)",
                    new { Username = username, Password = password, InfoId = infoId });
            }

            SetMessage("New teacher inserted!");
        }
        catch (Exception ex)
        {
            e.ExceptionHandled = true;
            SetMessage("Insert failed: " + ex.Message);
        }
    }


    protected void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ExceptionHandled = true;
            SetMessage("Delete failed! Reason: " + e.Exception.Message);
        }
        else
        {
            SetMessage("Student Information deleted!");
        }
    }

    protected void RadGrid1_DataBound()
    {
        System.Diagnostics.Debug.WriteLine("RadGrid1_DataBound fired");
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
                                u.[Username], 
                                u.[Password]
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
            DisplayMessage(gridMessage);
        }
    }

    protected void RadGrid2_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.KeepInEditMode = true;
            e.ExceptionHandled = true;
            SetMessage("Update failed. Reason: " + e.Exception.Message);
        }
        else
        {
            SetMessage("Teacher information updated!");
        }
    }

    protected void RadGrid2_ItemInserted(object source, GridInsertedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ExceptionHandled = true;
            SetMessage("Insert failed! Reason: " + e.Exception.Message);
        }
        else
        {
            SetMessage("New teacher is inserted!");
        }
    }

    protected void RadGrid2_ItemDeleted(object source, GridDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ExceptionHandled = true;
            SetMessage("Delete failed! Reason: " + e.Exception.Message);
        }
        else
        {
            SetMessage("Teacher information deleted!");
        }
    }

    protected void RadGrid2_DataBound()
    {
        System.Diagnostics.Debug.WriteLine("RadGrid2_DataBound fired");
    }
    #endregion

    

    #region Switch Status Handler
    protected void switchStatus_CheckedChanged(object sender, EventArgs e)
    {
        var switchControl = (RadSwitch)sender;
        var item = (GridDataItem)switchControl.NamingContainer;
        var userInfoId = Convert.ToInt32(item.GetDataKeyValue("ID"));
        var newStatus = (bool)switchControl.Checked? 1 : 0;

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

                    SetMessage($"Status updated to {(newStatus == 1 ? "Active" : "Inactive")} successfully!");
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating status: {ex.Message}");
            SetMessage("Error updating status: " + ex.Message);
        }
    }
    #endregion

    #region Helpers
    private bool IsUserExists(string fullName, string cityLive, string role)
    {
        using (var connection = new SqlConnection(conn))
        {
            var sql = "";
            if(role == "Student")
                sql = "SELECT COUNT(*) FROM StudentInfo WHERE FullName = @FullName AND CityLive = @CityLive";
            else
                sql = "SELECT COUNT(*) FROM TeacherInfo WHERE FullName = @FullName AND CityLive = @CityLive";
            var count = connection.ExecuteScalar<int>(sql, new { FullName = fullName, CityLive = cityLive });
            return count > 0;
        }
    }

    private string GenerateUsername(string fullName)
    {
        // Ex: "Nguyen Le Khanh" => "khanhnl"
        var parts = fullName.Trim().Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
        string lastName = parts[parts.Length - 1].ToLower();
        string initials = "";
        for (int i = 0; i < parts.Length - 1; i++)
        {
            initials += parts[i][0];
        }

        return $"{lastName}{initials.ToLower()}";
    }

    private void DisplayMessage(string text)
    {
        RadGrid1.Controls.Add(new LiteralControl(string.Format("<span style='color:red'>{0}</span>", text)));
    }

    private void SetMessage(string message)
    {
        gridMessage = message;
    }
    #endregion
}
