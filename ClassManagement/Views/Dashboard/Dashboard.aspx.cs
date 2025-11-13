using ClassManagement.Shared;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

public partial class Dashboard : BasePage
{
    private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    public class ScheduledClassStat
    {
        public string ScheduledClass { get; set; }
        public int ClassCount { get; set; }
    }
    public class ClassScheduleStat
    {
        public string Name { get; set; }
        public int TotalStudent { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ActiveBindPieChart();
            GetClassCountBySchedule();
            StudentOfClassesChart();
            SetChartByRole();
        }
    }

    private void SetChartByRole()
    {
        string role = Session["UserRole"] as string;
        RadHtmlChart1.Visible = (role == "Admin" || role == "Teacher");
        RadHtmlChart2.Visible = (role == "Admin" || role == "Teacher");
        RadHtmlChart3.Visible = (role == "Admin" || role == "Teacher");
        RadHtmlChartBar.Visible = (role == "Admin" || role == "Teacher");
    }

    private void ActiveBindPieChart()
    {
        int totalActiveStudent = 0;
        int totalInactiveStudent = 0;
        int totalActiveTeacher = 0;
        int totalInactiveTeacher = 0;

        using (var connection = new SqlConnection(_connectionString))
        {
            // Student status
            string totalActiveStudentSql = @"
                SELECT COUNT(*) FROM StudentInfo WHERE Status = 1;";
            string totalInactiveStudentSql = @"
                SELECT COUNT(*) FROM StudentInfo WHERE Status = 0;";
            totalActiveStudent = connection.ExecuteScalar<int>(totalActiveStudentSql);
            totalInactiveStudent = connection.ExecuteScalar<int>(totalInactiveStudentSql);
            // Teacher status
            string totalActiveTeacherSql = @"
                SELECT COUNT(*) FROM TeacherInfo WHERE Status = 1;";
            string totalInactiveTeacherSql = @"
                SELECT COUNT(*) FROM TeacherInfo WHERE Status = 0;";
            totalActiveTeacher = connection.ExecuteScalar<int>(totalActiveTeacherSql);
            totalInactiveTeacher = connection.ExecuteScalar<int>(totalInactiveTeacherSql);
        }

        // Browser statistics
        var browserUsage1 = new List<BrowserUsage>
        {
            new BrowserUsage { Browser = "Active", Value = totalActiveStudent , Explode = false },
            new BrowserUsage { Browser = "Inactive", Value = totalInactiveStudent, Explode = false },
        };
        var browserUsage2 = new List<BrowserUsage>
        {
            new BrowserUsage { Browser = "Active", Value = totalActiveTeacher , Explode = false },
            new BrowserUsage { Browser = "Inactive", Value = totalInactiveTeacher, Explode = false },
        };

        RadHtmlChart1.DataSource = browserUsage1;
        RadHtmlChart1.DataBind();

        RadHtmlChart2.DataSource = browserUsage2;
        RadHtmlChart2.DataBind();
    }

    public void GetClassCountBySchedule()
    {
        IEnumerable<ScheduledClassStat> classes;
        using (var connection = new SqlConnection(_connectionString))
        {
            //GROUP BY helps SQL automatically count each type of schedule without looping
            string sql = @"
                SELECT 
                    ScheduledClass, 
                    COUNT(*) AS ClassCount
                FROM [Class]
                WHERE ScheduledClass IS NOT NULL
                GROUP BY ScheduledClass 
                ORDER BY ScheduledClass;
            ";
            classes = connection.Query<ScheduledClassStat>(sql).ToList();
        }
        var browserUsage3 = new List<BrowserUsage>();

        foreach (var item in classes)
        {
            browserUsage3.Add(new BrowserUsage
            {
                Browser = item.ScheduledClass,
                Value = item.ClassCount,
                Explode = false
            });
        }
        RadHtmlChart3.DataSource = browserUsage3;
        RadHtmlChart3.DataBind();
    }

    private void StudentOfClassesChart()
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            string query = @"
                SELECT Name, TotalStudent
                FROM [Class];
            ";

            var data = conn.Query<ClassScheduleStat>(query).ToList();

            RadHtmlChartBar.DataSource = data;
            RadHtmlChartBar.DataBind();
        }
    }
}
public class BrowserUsage
{
    public string Browser { get; set; }
    public double Value { get; set; }
    public bool Explode { get; set; }
}