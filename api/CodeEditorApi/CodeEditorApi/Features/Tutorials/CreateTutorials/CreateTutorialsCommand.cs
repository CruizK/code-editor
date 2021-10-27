using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public CreateTutorialsCommand(ICreateTutorials createTutorials)
        {
            _createTutorials = createTutorials;
        }

        public async Task<ActionResult<Tutorial>> ExecuteAsync(CreateTutorialsBody createTutorialsBody)
        {
            return await _createTutorials.ExecuteAsync(createTutorialsBody);
        }
    }
}
