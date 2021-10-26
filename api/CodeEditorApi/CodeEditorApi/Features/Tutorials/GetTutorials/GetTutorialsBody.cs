using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public class GetTutorialsBody
    {
        [Required]
        public int TutorialId;

        [Required]
        public int UserId;
    }
}
