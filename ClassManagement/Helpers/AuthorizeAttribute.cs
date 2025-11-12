using System;

namespace ClassManagement.Helpers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AuthorizeAttribute : Attribute
    {
        public string[] Roles { get; set; }

        public AuthorizeAttribute(params string[] roles)
        {
            Roles = roles;
        }
    }

}