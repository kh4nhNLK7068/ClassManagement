using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ClassManagement.Helpers
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var path = context.Request.AppRelativeCurrentExecutionFilePath.ToLower();

            if (path.EndsWith(".axd") ||
                path.Contains("signinform.aspx") ||
                path.Contains("signupform.aspx") ||
                path.StartsWith("~/scripts") ||
                path.StartsWith("~/styles") ||
                path.StartsWith("~/images"))
            {
                return;
            }

            // Allow access to public pages
            if (path.Contains("SignInForm.aspx") || path.Contains("SignUpForm.aspx"))
                return;

            if (context.Session == null || context.Session["JwtToken"] == null)
            {
                context.Response.Redirect("~/Views/Account/SignInForm.aspx");
                return;
            }

            // If there is a token, check its validity.
            try
            {
                var principal = JwtManager.ValidateToken(context.Session["JwtToken"].ToString());
                HttpContext.Current.User = principal;
            }
            catch
            {
                context.Session.Clear();
                context.Response.Redirect("~/Views/Account/SignInForm.aspx");
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
