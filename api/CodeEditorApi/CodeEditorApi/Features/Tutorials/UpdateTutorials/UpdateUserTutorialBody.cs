using System;
using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Tutorials.UpdateTutorials
{
    public class UpdateUserTutorialBody
    {
        [Required]
        public bool InProgress { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public string UserCode { get; set; }
    }
}
