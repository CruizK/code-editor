using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features
{
    public class SearchInput
    {
        public string searchString {get; set;}

        public int difficultyId { get; set; }

        public int languageId { get; set; }

    }
}
