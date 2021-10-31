using CodeEditorApi.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Courses.CreateCourses
{
    public class CreateCourseBody
    {
        [RegularExpression(RegExp.CourseTitleExpression, ErrorMessage = "Title does not meet criteria")]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsPublished { get; set; }
        
    }
}
