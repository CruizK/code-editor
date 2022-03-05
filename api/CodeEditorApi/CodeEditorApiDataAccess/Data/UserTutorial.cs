using System;
using System.Collections.Generic;

#nullable disable

namespace CodeEditorApiDataAccess.Data
{
    public partial class UserTutorial
    {
        public int TutorialId { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string UserCode { get; set; }

        public virtual CfgTutorialStatus StatusNavigation { get; set; }
        public virtual Tutorial Tutorial { get; set; }
        public virtual User User { get; set; }
    }
}
