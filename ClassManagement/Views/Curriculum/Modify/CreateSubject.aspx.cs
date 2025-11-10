using System;
using System.Data.SqlClient;
using Dapper;

public partial class CreateSubject : System.Web.UI.Page
{
    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblFormTitle.Text = (Request.QueryString["id"] == null)
                ? "Create Subject"
                : "Update Subject";

            if (Request.QueryString["id"] != null)
                LoadSubject();
            else
            {
                statusField.Visible = false;
            }
        }
    }

    private void LoadSubject()
    {
        int id = Convert.ToInt32(Request.QueryString["id"]);
        using (var con = new SqlConnection(connStr))
        {
            var subject = con.QueryFirstOrDefault("SELECT * FROM Subject WHERE ID = @ID", new { ID = id });

            if (subject != null)
            {
                txtName.Text = subject.Name;
                txtDescription.Text = subject.Description;
                ddlType.Text = subject.Type;
                switchStatus.Checked = subject.Status == true;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        using (var con = new SqlConnection(connStr))
        {
            if (Request.QueryString["id"] == null)
            {
                // CREATE
                con.Execute(@"
                        INSERT INTO Subject (Name, Description, Type, Status)
                        VALUES (@Name, @Description, @Type, @Status)",
                    new
                    {
                        Name = txtName.Text,
                        Description = txtDescription.Text,
                        Type = ddlType.Text,
                        Status = 1
                    });
            }
            else
            {
                // UPDATE
                int id = Convert.ToInt32(Request.QueryString["id"]);
                con.Execute(@"
                        UPDATE Subject
                        SET Name = @Name, Description = @Description, Type = @Type, Status = @Status
                        WHERE ID = @ID",
                    new
                    {
                        ID = id,
                        Name = txtName.Text,
                        Description = txtDescription.Text,
                        Type = ddlType.Text,
                        Status = (bool)switchStatus.Checked ? 1 : 0
                    });

                //Change class status and then remove all students in the each class
                if ((bool)switchStatus.Checked == false)
                {
                    con.Execute(@"
                        BEGIN TRAN;

                        UPDATE Class
                        SET Status = 'Cancelled'
                        WHERE SubjectId = @SubjectId;

                        DELETE sic
                        FROM StudentInClass sic
                        INNER JOIN Class c ON c.ID = sic.ClassId
                        WHERE c.SubjectId = @SubjectId;

                        COMMIT;
                    ", new { SubjectId = id });
                }
            }
        }

        Response.Redirect("../Curriculum.aspx");
    }
}
