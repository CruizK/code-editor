using CodeEditorApi.Errors;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.CreateTutorials
{
    public interface ICreateTutorialsCommand
    {
        public Task<ActionResult<Tutorial>> ExecuteAsync(CreateTutorialsBody createTutorialsBody);
    }
    public class CreateTutorialsCommand : ICreateTutorialsCommand
    {
        private readonly ICreateTutorials _createTutorials;
        private readonly IGetTutorials _getTutorials;

        public CreateTutorialsCommand(ICreateTutorials createTutorials, IGetTutorials getTutorials)
        {
            _createTutorials = createTutorials;
            _getTutorials = getTutorials;
        }

        public async Task<ActionResult<Tutorial>> ExecuteAsync(CreateTutorialsBody createTutorialsBody)
        {
            var tutorialsInCourse = await _getTutorials.GetCourseTutorials(createTutorialsBody.CourseId);
            if (tutorialsInCourse.Find(t => t.Title == createTutorialsBody.Title) != null)
            {
                return ApiError.BadRequest($"A tutorial already exists under course {createTutorialsBody.CourseId} with the same title '{createTutorialsBody.Title}'");
            }
            return await _createTutorials.ExecuteAsync(createTutorialsBody);
        }
    }
}
