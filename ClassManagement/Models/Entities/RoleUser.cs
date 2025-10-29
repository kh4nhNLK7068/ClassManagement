using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassManagement.Models.Entities
{
    [Table("RoleUser")]
    public class RoleUser
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        //Navigation
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}