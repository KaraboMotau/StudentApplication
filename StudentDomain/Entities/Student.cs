using System.ComponentModel.DataAnnotations;

namespace StudentDomain.Entities
{
    public class Student
    {
        [Key]
        public int StudentNumber { get; set; }
        public string Surname { get; set; }
        public string Initials { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }     

        //[Display(Name = "Date of Birth")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public string Birthdate { get; set; }

        //1 to many
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Course> Courses { get; set; }

    }
}
