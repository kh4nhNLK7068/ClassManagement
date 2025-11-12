using ClassManagement.Helpers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;

namespace ClassManagement.Shared
{
    public class BasePage : Page
    {
        protected ClaimsPrincipal CurrentUser { get; private set; }
        protected string UserRole { get; private set; }
        protected int UserId { get; private set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Validate JWT Token
            ValidateAuthentication();
        }

        private void ValidateAuthentication()
        {
            string token = Session["JwtToken"] as string;

            if (string.IsNullOrEmpty(token))
            {
                // Try to get from Cookie
                HttpCookie authCookie = Request.Cookies["AuthToken"];
                token = authCookie?.Value;
            }

            if (string.IsNullOrEmpty(token))
            {
                RedirectToLogin();
                return;
            }

            // Validate token
            CurrentUser = JwtManager.ValidateToken(token);

            if (CurrentUser == null)
            {
                RedirectToLogin();
                return;
            }

            // Get user information from claims
            UserRole = CurrentUser.FindFirst(ClaimTypes.Role)?.Value;
            UserId = int.Parse(CurrentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Check authorization
            CheckAuthorization();
        }

        // Optional: Override this method in child pages to check specific rights.
        protected virtual void CheckAuthorization()
        {
            var authorizeAttr = (AuthorizeAttribute)Attribute.GetCustomAttribute(
                this.GetType(), typeof(AuthorizeAttribute));

            if (authorizeAttr != null && authorizeAttr.Roles.Length > 0)
            {
                if (!IsInRole(authorizeAttr.Roles))
                {
                    Response.Redirect("~/Unauthorized.aspx");
                }
            }
        }


        protected void RedirectToLogin()
        {
            Response.Redirect("~/Views/Account/SignInForm.aspx");
        }

        protected bool IsInRole(params string[] roles)
        {
            return roles.Contains(UserRole, StringComparer.OrdinalIgnoreCase);
        }
    }
}