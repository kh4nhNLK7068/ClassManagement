using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassManagement.Models.Entities
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required, StringLength(50)]
        public string Username { get; set; }
        [Required, StringLength(255)]
        public string Password { get; set; }
        [Required]
        public bool Status { get; set; } = true;
        [ForeignKey("TeacherInfo")]
        public int TeacherInfoId { get; set; }
        [ForeignKey("StudentInfo")]
        public int StudentInfoId { get; set; }

        //Navigation
        public virtual TeacherInfo TeacherInfo { get; set; }
        public virtual StudentInfo StudentInfo { get; set; }
        public virtual ICollection<RoleUser> RoleUsers { get; set; }
    }
}