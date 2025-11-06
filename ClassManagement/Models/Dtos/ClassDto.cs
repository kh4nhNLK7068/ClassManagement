using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassManagement.Models.Dtos
{
    public class ClassDto
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string SubjectName { get; set; }
        public string ScheduledClass { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public int TotalStudent { get; set; }
        public string Status { get; set; }
    }
}