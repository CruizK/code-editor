using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetCourseTutorialsCommand
    {
        public Task<ActionResult<List<Tutorial>>> ExecuteAsync(int courseId);
    }
    public class GetCourseTutorialsCommand : IGetCourseTutorialsCommand
    {
        private readonly IGetTutorials _getTutorials;

        public GetCourseTutorialsCommand(IGetTutorials getTutorials)
        {
            _getTutorials = getTutorials;
        }
        public async Task<ActionResult<List<Tutorial>>> ExecuteAsync(int courseId)
        {
            return await _getTutorials.GetCourseTutorials(courseId);
        }
    }
}
