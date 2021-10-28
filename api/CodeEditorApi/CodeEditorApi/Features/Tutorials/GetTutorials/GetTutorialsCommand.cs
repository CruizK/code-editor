using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var tutorials = await _getTutorials.GetUserTutorials(tutorialId);
            return tutorials;
        }
    }
}
