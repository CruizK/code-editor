using System;
using System.Collections.Generic;

#nullable disable

namespace CodeEditorApiDataAccess.Data
{
    public partial class CfgDifficultyLevel
    {
        public CfgDifficultyLevel()
        {
            Tutorials = new HashSet<Tutorial>();
        }

        public int Id { get; set; }
        public string Difficulty { get; set; }

        public virtual ICollection<Tutorial> Tutorials { get; set; }
    }
}
