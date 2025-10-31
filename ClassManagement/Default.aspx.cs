using ClassManagement.Helpers;
using ClassManagement.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassManagement
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["JwtToken"] == null)
            {
                Response.Redirect("~/SignInForm.aspx");
                return;
            }

            try
            {
                var principal = JwtManager.ValidateToken(Session["JwtToken"].ToString());
                var role = principal.FindFirst(System.Security.Claims.ClaimTypes.Role).Value;
            }
            catch
            {
                Response.Redirect("~/SignInForm.aspx");
            }
        }
    }
}