using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassManagement.Models.Dtos
{
    public class UserInfoDto
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CityLive { get; set; }
        public bool Active { get; set; }
        public string Username { get; set; }
    }
}