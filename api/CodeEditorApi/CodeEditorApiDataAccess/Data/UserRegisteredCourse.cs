using System;
using System.Collections.Generic;

#nullable disable

namespace CodeEditorApiDataAccess.Data
{
    public partial class UserRegisteredCourse
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
