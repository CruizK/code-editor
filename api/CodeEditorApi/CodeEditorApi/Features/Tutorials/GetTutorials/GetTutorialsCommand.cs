using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeEditorApiDataAccess.Data;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetTutorialsCommand
    {
        public Task<Tutorial> ExecuteAsync(GetTutorialsBody getTutorialsBody);
    }
    public class GetTutorialsCommand : IGetTutorialsCommand
    {
        private readonly IGetTutorials _getTutorials;

        public GetTutorialsCommand(IGetTutorials getTutorials)
        {
            _getTutorials = getTutorials;
        }
        public async Task<Tutorial> ExecuteAsync(GetTutorialsBody getTutorialsBody)
        {
            var tutorials = await _getTutorials.GetUserTutorials(getTutorialsBody);
            return tutorials;
        }
    }
}
