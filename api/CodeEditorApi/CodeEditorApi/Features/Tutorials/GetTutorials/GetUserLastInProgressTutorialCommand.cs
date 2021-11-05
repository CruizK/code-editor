using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetUserLastInProgressTutorialCommand
    {
        public Task<ActionResult<Tutorial>> ExecuteAsync(int userId, int courseId);
    }
    public class GetUserLastInProgressTutorialCommand : IGetUserLastInProgressTutorialCommand
    {
        private readonly IGetTutorials _getTutorials;

        public GetUserLastInProgressTutorialCommand(IGetTutorials getTutorials)
        {
            _getTutorials = getTutorials;
        }

        public async Task<ActionResult<Tutorial>> ExecuteAsync(int userId, int courseId)
        {
            return await _getTutorials.GetUserLastInProgressTutorial(userId, courseId);
        }
    }
}
