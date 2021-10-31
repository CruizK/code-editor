using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetUserCreatedTutorialsCommand
    {
        public Task<ActionResult<List<Tutorial>>> ExecuteAsync(int userId);
    }
    public class GetUserCreatedTutorialsCommand : IGetUserCreatedTutorialsCommand
    {
        private readonly IGetTutorials _getTutorials;

        public GetUserCreatedTutorialsCommand(IGetTutorials getTutorials)
        {
            _getTutorials = getTutorials;
        }
        public async Task<ActionResult<List<Tutorial>>> ExecuteAsync(int userId)
        {
            return await _getTutorials.GetUserCreatedTutorials(userId);
        }
    }
}
