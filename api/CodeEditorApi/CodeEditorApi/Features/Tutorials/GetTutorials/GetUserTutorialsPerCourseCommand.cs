using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetUserTutorialsPerCourseCommand
    {
        public Task<ActionResult<List<UserTutorial>>> ExecuteAsync(int courseId, int userId);
    }
    public class GetUserTutorialsPerCourseCommand : IGetUserTutorialsPerCourseCommand
    {
        private readonly IGetTutorials _getTutorials;

        public GetUserTutorialsPerCourseCommand(IGetTutorials getTutorials)
        {
            _getTutorials = getTutorials;
        }
        public async Task<ActionResult<List<UserTutorial>>> ExecuteAsync(int courseId, int userId)
        {
            return await _getTutorials.GetUserRegisteredTutorials(courseId, userId);
        }
    }
}
