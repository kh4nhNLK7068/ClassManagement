using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassManagement.Models.Entities
{
    [Table("StudentInClass")]
    public class StudentInClass
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        [ForeignKey("StudentInfo")]
        public int StudentId { get; set; }

        //Navigation
        public virtual Class Class { get; set; }
        public virtual StudentInfo StudentInfo { get; set; }

    }
}