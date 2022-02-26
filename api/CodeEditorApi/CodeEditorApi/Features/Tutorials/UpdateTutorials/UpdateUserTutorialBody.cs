using System;
using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Tutorials.UpdateTutorials
{
    public class UpdateUserTutorialBody
    {
        [Required]
        public int TutorialStatus { get; set; }

        [Required]
        public string UserCode { get; set; }
    }
}
