
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using StudentDomain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace StudentApplicationServer.DataAccess
{
    public class StudentApplicationDbContext : IdentityDbContext
    {

        public StudentApplicationDbContext(DbContextOptions<StudentApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentNumber = 12345, Surname = "Motau", Birthdate = "23/10/1990", Gender = "M", Initials = "KT" },
                new Student { StudentNumber = 15487, Surname = "Mokoena", Birthdate = "6/11/1991", Gender = "F", Initials = "LS" },
                new Student { StudentNumber = 19548, Surname = "Malan", Birthdate = "18/9/1992", Gender = "M", Initials = "TG" },
                new Student { StudentNumber = 13215, Surname = "Moodley", Birthdate = "23/4/2000", Gender = "F", Initials = "PC" }

                );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { EnrollmentId = 1, Status = "Pending" },
                new Enrollment { EnrollmentId = 2, Status = "Accepted" },
                new Enrollment { EnrollmentId = 3, Status = "Declined" },
                new Enrollment { EnrollmentId = 4, Status = "Pending" }

                );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseCode = 1011, CourseName = "Computer Systems Engineering" },
                new Course { CourseCode = 1012, CourseName = "Language Practice" },
                new Course { CourseCode = 1013, CourseName = "Electrical Engineering" },
                new Course { CourseCode = 1014, CourseName = "Accounting" }

                );

            modelBuilder.Entity<Faculty>().HasData(
                 new Faculty { FacultyCode = 101, FacultyName = "Information Communication Technology" },
                 new Faculty { FacultyCode = 201, FacultyName = "Engineering and Sciences" },
                 new Faculty { FacultyCode = 301, FacultyName = "Economics" },
                 new Faculty { FacultyCode = 401, FacultyName = "ICT" },
                 new Faculty { FacultyCode = 501, FacultyName = "Humanities" }

                 );



        }
        */
    }
}