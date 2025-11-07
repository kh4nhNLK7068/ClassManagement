using ClassManagement.Models.Dtos;
using ClassManagement.Models.Entities;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;
using Telerik.Web.UI;

public partial class Curriculum : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RadGridSubject_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        using (var connection = new SqlConnection(conn))
        {
            var sql = @"
                SELECT s.ID,
                    s.[Name] AS Name,
                    s.[Description] AS Description,
                    s.[Type] AS Type,
                    s.[Status] AS Status
                FROM Subject s;
            ";
            var data = connection.Query<Subject>(sql).ToList();
            RadGridSubject.DataSource = data;
        }
    }

    protected void RadGridClass_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {

        var parentItem = (Telerik.Web.UI.GridDataItem)e.DetailTableView.ParentItem;
        if (e.DetailTableView.Name == "Classes")
        {
            int subjectId = Convert.ToInt32(parentItem.GetDataKeyValue("ID"));
            using (var connection = new SqlConnection(conn))
            {
                var sql = @"
                    SELECT c.ID,
                       c.[Name] AS Name,
                       c.[Type] AS Type,
                       subj.[Name] AS SubjectName,
                       c.ScheduledClass,
                       CONVERT(VARCHAR(5), c.TimeStart, 108) AS TimeStart,
                       CONVERT(VARCHAR(5), c.TimeEnd, 108) AS TimeEnd,
                       c.TotalStudent,
                       c.Status
                    FROM [Class] c
                    INNER JOIN Subject subj ON c.SubjectId = subj.ID
                    WHERE c.SubjectId = @subjectId
                    ORDER BY c.SubjectId;
                ";

                e.DetailTableView.DataSource = connection.Query<ClassDto>(sql, new { subjectId = subjectId });
            }
        }

        if (e.DetailTableView.Name == "Students")
        {
            int classId = Convert.ToInt32(parentItem.GetDataKeyValue("ID"));
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

                e.DetailTableView.DataSource = connection.Query<StudentInfo>(sql, new { ClassId = classId });
            }
        }
    }

    protected void RadGridSubject_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "EditSubject")
        {
            int subjectId = Convert.ToInt32((e.Item as GridDataItem).GetDataKeyValue("ID"));
            Response.Redirect($"~/Views/Curriculum/Modify/CreateSubject.aspx?id={subjectId}");
        }

        if (e.CommandName == "EditClass")
        {
            int classId = Convert.ToInt32((e.Item as GridDataItem).GetDataKeyValue("ID"));
            Response.Redirect($"~/Views/Class/EditClass.aspx?id={classId}");
        }

        if (e.CommandName == "EditStudent")
        {
            int studentId = Convert.ToInt32((e.Item as GridDataItem).GetDataKeyValue("ID"));
            Response.Redirect($"~/Views/Student/EditStudent.aspx?id={studentId}");
        }
    }
    protected void ddlCreateNew_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        switch (e.Value)
        {
            case "Subject":
                Response.Redirect("~/Views/Curriculum/Modify/CreateSubject.aspx");
                break;
            case "Class":
                Response.Redirect("~/Views/Class/CreateClass.aspx");
                break;
            case "Student":
                Response.Redirect("~/Views/Student/CreateStudent.aspx");
                break;
        }
    }

}
