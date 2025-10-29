using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassManagement.Models.Entities
{
    [Table("Class")]
    public class Class
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }

        public string ScheduledClass { get; set; }
        public int TotalStudent { get; set; }
        public bool Status { get; set; }

        // Navigation
        /*
         Use a regular when the relationship is one-to-one or many-to-one - meaning the entity only references a single record.
         Ex: FK (many-to-one: Each Class belongs to only one Subject)
         */
        public virtual Subject Subject { get; set; }
        /*
         Use ICollection<T> when the relationship is one-to-many or many-to-many - meaning an entity contains many child records.
         Ex: PK (one-to-many: A Class can contain multiple StudentInClass)
         */
        public virtual ICollection<StudentInClass> StudentInClasses { get; set; }
    }
}