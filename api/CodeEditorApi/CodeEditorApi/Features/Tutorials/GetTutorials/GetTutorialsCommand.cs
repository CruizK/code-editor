using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeEditorApi.Errors;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetTutorialsCommand
    {
        public Task<ActionResult<Tutorial>> ExecuteAsync(int tutorialId);
    }
    public class GetTutorialsCommand : IGetTutorialsCommand
    {
        private readonly IGetTutorials _getTutorials;

        public GetTutorialsCommand(IGetTutorials getTutorials)
        {
            _getTutorials = getTutorials;
        }
        public async Task<ActionResult<Tutorial>> ExecuteAsync(int tutorialId)
        {
            var tutorial = await _getTutorials.GetUserTutorials(tutorialId);
            if (tutorial.Result == null) return ApiError.BadRequest($"Cannot retrieve Tutorial with id {tutorialId}");
            return tutorial;
        }
    }
}
