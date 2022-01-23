using CodeEditorApi.Features.Tutorials.UpdateTutorials;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetUserTutorialsDetailsCommand
    {
        public Task<ActionResult<List<UserTutorialDetailsBody>>> ExecuteAsync(int courseId, int userId);
    }
    public class GetUserTutorialsDetailsCommand : IGetUserTutorialsDetailsCommand
    {
        private readonly IGetTutorials _getTutorials;

        public GetUserTutorialsDetailsCommand(IGetTutorials getTutorials)
        {
            _getTutorials = getTutorials;
        }
        public async Task<ActionResult<List<UserTutorialDetailsBody>>> ExecuteAsync(int courseId, int userId)
        {
            return await _getTutorials.GetUserRegisteredTutorialsWithCourseTutorialDetails(courseId, userId);
        }
    }
}
