using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.UpdateTutorials
{
    public interface IUpdateTutorialsCommand
    {
        public Task<ActionResult<Tutorial>> ExecuteAsync(int tutorialId, CreateTutorialsBody createTutorialsBody);
    }
    public class UpdateTutorialsCommand : IUpdateTutorialsCommand
    {
        private readonly IUpdateTutorials _updateTutorials;

        public UpdateTutorialsCommand(IUpdateTutorials updateTutorials)
        {
            _updateTutorials = updateTutorials;
        }

        public async Task<ActionResult<Tutorial>> ExecuteAsync(int tutorialId, CreateTutorialsBody createTutorialsBody)
        {
            return await _updateTutorials.ExecuteAsync(tutorialId, createTutorialsBody);
        }
    }
}
