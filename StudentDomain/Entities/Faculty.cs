using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDomain.Entities
{
    public class Faculty
    {
        [Key]
        public int FacultyCode { get; set; }
        public string FacultyName { get; set; }
   
        public ICollection<Course> Courses { get; set; }
    }
}
