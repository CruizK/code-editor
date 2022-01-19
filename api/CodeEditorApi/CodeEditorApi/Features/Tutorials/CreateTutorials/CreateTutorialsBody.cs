using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Tutorials.CreateTutorials
{
    public class CreateTutorialsBody
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public int Author { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public bool IsPublished { get; set; }

        public string Description { get; set; }

        public string Prompt { get; set; }

        public int? LanguageId { get; set; }  //should eventually be required and NOT NULLable

        public int? DifficultyId { get; set; } //should eventually be required and NOT NULLable

        public int? Index { get; set; }   //should eventually be required and NOT NULLable

    }
}
