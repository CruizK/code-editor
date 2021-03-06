using System;
using System.Collections.Generic;

#nullable disable

namespace CodeEditorApiDataAccess.Data
{
    public partial class User
    {
        public User()
        {
            Courses = new HashSet<Course>();
            Tutorials = new HashSet<Tutorial>();
            UserRegisteredCourses = new HashSet<UserRegisteredCourse>();
            UserTutorials = new HashSet<UserTutorial>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string AccessToken { get; set; }
        public int RoleId { get; set; }
        public bool IsConfirmed { get; set; }

        public virtual CfgRole Role { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Tutorial> Tutorials { get; set; }
        public virtual ICollection<UserRegisteredCourse> UserRegisteredCourses { get; set; }
        public virtual ICollection<UserTutorial> UserTutorials { get; set; }
    }
}
