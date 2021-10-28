using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.DeleteTutorials
{
    public interface IDeleteTutorialsCommand
    {
        public Task<ActionResult<Tutorial>> ExecuteAsync(int tutorialId);
    }
    public class DeleteTutorialsCommand : IDeleteTutorialsCommand
    {
        private readonly IDeleteTutorials _deleteTutorials;

        public DeleteTutorialsCommand(IDeleteTutorials deleteTutorials)
        {
            _deleteTutorials = deleteTutorials;   
        }

        public async Task<ActionResult<Tutorial>> ExecuteAsync(int tutorialId)
        {
            return await _deleteTutorials.ExecuteAsync(tutorialId);
        }
    }
}
