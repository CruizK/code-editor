using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Tutorials.UpdateTutorials
{
    public class UpdateUserTutorialBody
    {
        public bool InProgress { get; set; }

        public bool IsCompleted { get; set; }
    }
}
