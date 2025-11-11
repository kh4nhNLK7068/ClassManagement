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
                                t.[Status] AS Active, 
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

    protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
    {
        if (e.Exception != null)
        {
            e.ExceptionHandled = true;
            SetMessage("Insert failed! Reason: " + e.Exception.Message);
        }
        else
        {
            SetMessage("New student is inserted!");
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
                                t.Status AS Active, 
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

    private void DisplayMessage(string text)
    {
        RadGrid1.Controls.Add(new LiteralControl(string.Format("<span style='color:red'>{0}</span>", text)));
    }

    private void SetMessage(string message)
    {
        gridMessage = message;
    }
}
