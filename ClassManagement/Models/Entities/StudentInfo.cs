using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Models.Entities
{
    [Table("StudentInfo")]
    public class StudentInfo
    {
        [Key]
        public int ID { get; set; }
        [Required, StringLength(100)]
        public string FullName { get; set; }
        public DateTime DoB { get; set; }
        [StringLength(100)]
        public string CityLive { get; set; }
        [Required]
        public bool Status { get; set; } = true;

        //Navigation
        public virtual ICollection<StudentInClass> StudentInClasses { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}