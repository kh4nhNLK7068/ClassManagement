using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassManagement.Models.Entities
{
    [Table("Subject")]
    public class Subject
    {

        [Key]
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public bool Status { get; set; }

        // Navigation
        public virtual ICollection<Class> Classes { get; set; }
    }
}