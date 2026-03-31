using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDomain.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string CourseCode { get; set; } 

        public ICollection<Student> Students { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

      

        public int FacultyCode { get; set; }
        public Faculty Faculties { get; set; }

    }
}
