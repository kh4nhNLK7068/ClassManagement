using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using ClassManagement.Models.Dtos;
using Dapper;
using Telerik.Web.UI;

public partial class CreateClass : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSubjectList();

            lblFormTitle.Text = Request.QueryString["id"] == null ? "Create Class" : "Update Class";

            if (Request.QueryString["id"] != null)
                LoadClass();
        }
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

            autoSchedule.Entries.Clear();
            foreach (var day in cls.ScheduledClass.Split(','))
            {
                string d = day.Trim();
                if (!string.IsNullOrWhiteSpace(d))
                    autoSchedule.Entries.Add(new AutoCompleteBoxEntry(d, d));
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
            var selectedDays = new List<string>();
            foreach (AutoCompleteBoxEntry entry in autoSchedule.Entries)
            {
                selectedDays.Add(entry.Value);
            }
            var schedule = string.Join(",", selectedDays);

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
                    Schedule = schedule,
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
                    Schedule = schedule,
                    Status = ddlStatus.SelectedValue
                });
            }
        }

        Response.Redirect("../Curriculum.aspx");
    }

}

