using System;
using System.Collections.Generic;

#nullable disable

namespace CodeEditorApiDataAccess.Data
{
    public partial class Course
    {
        public Course()
        {
            Tutorials = new HashSet<Tutorial>();
            UserRegisteredCourses = new HashSet<UserRegisteredCourse>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int Author { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool IsPublished { get; set; }

        public virtual User AuthorNavigation { get; set; }
        public virtual ICollection<Tutorial> Tutorials { get; set; }
        public virtual ICollection<UserRegisteredCourse> UserRegisteredCourses { get; set; }
    }
}
