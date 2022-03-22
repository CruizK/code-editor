using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public class SearchTutorialsBody
    {
        public string Title { get; set; }
        public int? LanguageId { get; set; }
        public int? DifficultyId { get; set; }
    }
}
