using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDomain.Entities
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public string Status { get; set; }

        public int StudentNumber { get; set; }
        public Student Students { get; set; }
        public int CourseCode { get; set; }
        
    }
}
