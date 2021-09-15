using System;
using System.Collections.Generic;

#nullable disable

namespace CodeEditorApiDataAccess.Data
{
    public partial class UserTutorial
    {
        public int TutorialId { get; set; }
        public int UserId { get; set; }
        public bool InProgress { get; set; }
        public bool IsCompleted { get; set; }

        public virtual Tutorial Tutorial { get; set; }
        public virtual User User { get; set; }
    }
}
