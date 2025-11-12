using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ClassManagement.Helpers;
using ClassManagement.Models.Dtos;
using ClassManagement.Models.Entities;
using ClassManagement.Shared;
using Dapper;

namespace ClassManagement.Views
{
    [Authorize("Student")]
    public partial class MyClass : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void RadGridMyClass_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string Username = (String)Session["Username"];
                var userIdSql = @"
                    SELECT [ID] FROM [User] WHERE Username = @Username;
                ";
                var userId = connection.Query<int>(userIdSql, new { Username = Username });

                var sql = @"
                    SELECT DISTINCT
                       c.ID,
                       c.[Name] AS Name,
                       c.[Type] AS Type,
                       subj.[Name] AS SubjectName,
                       c.ScheduledClass,
                       CONVERT(VARCHAR(5), c.TimeStart, 108) AS TimeStart,
                       CONVERT(VARCHAR(5), c.TimeEnd, 108) AS TimeEnd,
                       c.TotalStudent,
                       c.Status
                    FROM [User] u
                    INNER JOIN StudentInfo si ON u.StudentInfoId = si.ID
                    INNER JOIN StudentInClass sc ON si.ID = sc.StudentId
                    INNER JOIN [Class] c ON sc.ClassId = c.ID
                    INNER JOIN Subject subj ON c.SubjectId = subj.ID
                    WHERE u.ID = @UserId
                    ORDER BY c.ID;
                ";
                var data = connection.Query<ClassDto>(sql, new { UserId = userId }).ToList();
                RadGridMyClass.DataSource = data;
            }
        }

        protected void RadGridMyClass_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            var parentItem = (Telerik.Web.UI.GridDataItem)e.DetailTableView.ParentItem;
            int classId = Convert.ToInt32(parentItem.GetDataKeyValue("ID"));

            using (var connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
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
}
