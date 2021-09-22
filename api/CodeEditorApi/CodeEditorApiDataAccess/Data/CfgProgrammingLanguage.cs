using System;
using System.Collections.Generic;

#nullable disable

namespace CodeEditorApiDataAccess.Data
{
    public partial class CfgProgrammingLanguage
    {
        public CfgProgrammingLanguage()
        {
            Tutorials = new HashSet<Tutorial>();
        }

        public int Id { get; set; }
        public string Language { get; set; }

        public virtual ICollection<Tutorial> Tutorials { get; set; }
    }
}
