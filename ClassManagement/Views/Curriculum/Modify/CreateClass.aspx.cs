using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassManagement.Models.Dtos;
using ClassManagement.Models.Entities;
using Dapper;
using Telerik.Web.UI;

public partial class CreateClass : System.Web.UI.Page
{
    private string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSubjectList();
            lblFormTitle.Text = Request.QueryString["id"] == null ? "Create Class" : "Update Class";

            if (Request.QueryString["id"] != null)
            {
                LoadClass();
                if (ddlStatus.SelectedValue == "Cancelled")
                    addStudentSection.Visible = false;
            }
            else
            {
                statusField.Visible = false;
                RadGridStudent.Visible = false;
                addStudentSection.Visible = false;
            }
        }
    }

    // Search student by name
    protected void txtSearchStudent_TextChanged(object sender, EventArgs e)
    {
        string searchText = txtSearchStudent.Text.Trim();

        if (string.IsNullOrEmpty(searchText) || searchText.Length < 2)
        {
            rptSearchResults.DataSource = null;
            rptSearchResults.DataBind();
            pnlSearchResults.Style["display"] = "none";
            return;
        }

        int classId = Convert.ToInt32(Request.QueryString["id"]);

        using (var connection = new SqlConnection(conn))
        {
            // Find students not in class yet
            var sql = @"
                SELECT TOP 10 
                    si.ID,
                    si.FullName,
                    si.DoB,
                    si.CityLive,
                    si.Status
                FROM StudentInfo si
                WHERE si.FullName LIKE @SearchText
                AND si.ID NOT IN (
                    SELECT StudentId 
                    FROM StudentInClass 
                    WHERE ClassId = @ClassId
                )
                ORDER BY si.FullName;
            ";

            var results = connection.Query<StudentInfo>(sql, new
            {
                SearchText = "%" + searchText + "%",
                ClassId = classId
            }).ToList();

            rptSearchResults.DataSource = results;
            rptSearchResults.DataBind();

            if (results.Any())
            {
                pnlSearchResults.Style["display"] = "block";
                ScriptManager.RegisterStartupScript(this, GetType(), "showResults", "showSearchResults();", true);
            }
            else
            {
                pnlSearchResults.Style["display"] = "none";
            }
        }
    }

    // Select student from search results
    protected void btnSelectStudent_Click(object sender, EventArgs e)
    {
        var btn = (LinkButton)sender;
        int studentId = Convert.ToInt32(btn.CommandArgument);

        using (var connection = new SqlConnection(conn))
        {
            var student = connection.QueryFirstOrDefault<StudentInfo>(
                "SELECT * FROM StudentInfo WHERE ID = @ID",
                new { ID = studentId }
            );

            if (student != null)
            {
                hfSelectedStudentId.Value = student.ID.ToString();
                lblSelectedStudent.Text = $"{student.FullName} - DoB: {student.DoB:dd-MM-yyyy} - {student.CityLive}";
                pnlSelectedStudent.Visible = true;

                txtSearchStudent.Text = student.FullName;
                pnlSearchResults.Style["display"] = "none";
            }
        }
    }

    // Add selected student to class
    protected void btnAddStudent_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfSelectedStudentId.Value))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('Please search and select a student first!');", true);
            return;
        }

        int studentId = Convert.ToInt32(hfSelectedStudentId.Value);
        int classId = Convert.ToInt32(Request.QueryString["id"]);

        using (var connection = new SqlConnection(conn))
        {
            // Check if student already in class
            var exists = connection.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM StudentInClass WHERE ClassId = @ClassId AND StudentId = @StudentId",
                new { ClassId = classId, StudentId = studentId }
            );

            if (exists > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('Student is already in this class!');", true);
                return;
            }

            // Add student to class
            var sql = @"
                INSERT INTO StudentInClass (ClassId, StudentId)
                VALUES (@ClassId, @StudentId)
            ";

            connection.Execute(sql, new { ClassId = classId, StudentId = studentId });

            // Reset form
            txtSearchStudent.Text = "";
            hfSelectedStudentId.Value = "";
            pnlSelectedStudent.Visible = false;

            // Refresh grid
            RadGridStudent.Rebind();

            ScriptManager.RegisterStartupScript(this, GetType(), "success",
                "alert('Student added successfully!');", true);
        }
    }

    // Load students in class
    protected void RadGridStudent_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] != null)
            {
                int classId = Convert.ToInt32(Request.QueryString["id"]);
                using (var connection = new SqlConnection(conn))
                {
                    var sql = @"
                        SELECT si.ID,
                               si.FullName,
                               si.DoB,
                               si.CityLive,
                               si.Status
                        FROM StudentInClass sc
                        INNER JOIN StudentInfo si ON sc.StudentId = si.ID
                        WHERE sc.ClassId = @ClassId
                        ORDER BY si.FullName;
                    ";
                    var data = connection.Query<StudentInfo>(sql, new { ClassId = classId });
                    RadGridStudent.DataSource = data;
                }
            }
            else
            {
                RadGridStudent.DataSource = new List<StudentInfo>();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            RadGridStudent.DataSource = new List<StudentInfo>();
        }
    }

    // Remove student from class
    protected void RadGridStudent_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        var item = (GridDataItem)e.Item;
        int studentId = Convert.ToInt32(item.GetDataKeyValue("ID"));
        int classId = Convert.ToInt32(Request.QueryString["id"]);

        using (var connection = new SqlConnection(conn))
        {
            var sql = "DELETE FROM StudentInClass WHERE ClassId = @ClassId AND StudentId = @StudentId";
            connection.Execute(sql, new { ClassId = classId, StudentId = studentId });
        }

        RadGridStudent.Rebind();
    }

    private void LoadSubjectList()
    {
        using (var con = new SqlConnection(conn))
        {
            ddlSubject.DataSource = con.Query<SubjectOptionDto>(
                "SELECT ID, Name FROM Subject WHERE Status = 1"
            ).ToList();
            ddlSubject.DataTextField = "Name";
            ddlSubject.DataValueField = "ID";
            ddlSubject.DataBind();
        }
    }

    private void LoadClass()
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);
        using (var con = new SqlConnection(conn))
        {
            var cls = con.QueryFirstOrDefault("SELECT * FROM Class WHERE ID = @id", new { id });
            txtName.Text = cls.Name;
            ddlType.SelectedValue = cls.Type;
            ddlSubject.SelectedValue = cls.SubjectId.ToString();

            if (!string.IsNullOrEmpty(cls.ScheduledClass))
            {
                var schedules = cls.ScheduledClass.Split(',');
                foreach (RadComboBoxItem item in cboSchedule.Items)
                {
                    if (Array.IndexOf(schedules, item.Value) >= 0)
                    {
                        item.Checked = true;
                    }
                }
            }

            tpStart.SelectedDate = DateTime.Today.Add(cls.TimeStart);
            tpEnd.SelectedDate = DateTime.Today.Add(cls.TimeEnd);
            ddlStatus.SelectedValue = cls.Status;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (var con = new SqlConnection(conn))
        {
            var selectedValues = new List<string>();
            foreach (RadComboBoxItem item in cboSchedule.Items)
            {
                if (item.Checked)
                {
                    selectedValues.Add(item.Value);
                }
            }
            selectedValues.Sort();

            if (Request.QueryString["id"] == null)
            {
                con.Execute(@"
                INSERT INTO Class (Name, Type, SubjectId, TimeStart, TimeEnd, ScheduledClass, Status)
                VALUES (@Name, @Type, @SubjectId, @TimeStart, @TimeEnd, @Schedule, @Status)",
                new
                {
                    Name = txtName.Text,
                    Type = ddlType.SelectedValue,
                    SubjectId = ddlSubject.SelectedValue,
                    TimeStart = tpStart.SelectedDate.Value.TimeOfDay,
                    TimeEnd = tpEnd.SelectedDate.Value.TimeOfDay,
                    Schedule = string.Join(",", selectedValues),
                    Status = ddlStatus.SelectedValue
                });
            }
            else
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                con.Execute(@"
                UPDATE Class SET
                    Name=@Name, Type=@Type, SubjectId=@SubjectId,
                    TimeStart=@TimeStart, TimeEnd=@TimeEnd, 
                    ScheduledClass=@Schedule, Status=@Status
                WHERE ID=@ID",
                new
                {
                    ID = id,
                    Name = txtName.Text,
                    Type = ddlType.SelectedValue,
                    SubjectId = ddlSubject.SelectedValue,
                    TimeStart = tpStart.SelectedDate.Value.TimeOfDay,
                    TimeEnd = tpEnd.SelectedDate.Value.TimeOfDay,
                    Schedule = string.Join(",", selectedValues),
                    Status = ddlStatus.SelectedValue
                });

                if (ddlStatus.SelectedValue == "Cancelled")
                {
                    con.Execute(@"
                        DELETE FROM StudentInClass
                        WHERE ClassId = @ID
                    ",
                    new { ID = id });
                }
                //else
                //{
                //    con.Execute(@"
                //    UPDATE s
                //    SET Status=@Status
                //    FROM Subject s
                //    INNER JOIN Class c ON c.SubjectId = s.ID
                //    WHERE c.ID=@ID",
                //    new
                //    {
                //        ID = id,
                //        Status = 1
                //    });
                //}
            }
        }

        Response.Redirect("../Curriculum.aspx");
    }
}