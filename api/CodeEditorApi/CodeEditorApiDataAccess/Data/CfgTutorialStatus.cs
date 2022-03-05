using System;
using System.Collections.Generic;

#nullable disable

namespace CodeEditorApiDataAccess.Data
{
    public partial class CfgTutorialStatus
    {
        public CfgTutorialStatus()
        {
            UserTutorials = new HashSet<UserTutorial>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<UserTutorial> UserTutorials { get; set; }
    }
}
