using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetTutorials
    {
        public Task<IEnumerable<Tutorial>> GetUserTutorials(GetTutorialsBody getTutorialsBody);

        public Task<Tutorial> GetUserCreatedTutorials(GetTutorialsBody getTutorialsBody);
    }
    public class GetTutorials : IGetTutorials
    {
        private readonly CodeEditorApiContext _context;

        public GetTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<Tutorial> GetUserCreatedTutorials(GetTutorialsBody getTutorialsBody)
        {
            var tutorial = await _context.Tutorials.FindAsync(getTutorialsBody.TutorialId);

            if (tutorial == null) throw new Exception($"There is no Tutorial that exists with Id {getTutorialsBody.TutorialId}");
            else return tutorial;
        }

        public Task<IEnumerable<Tutorial>> GetUserTutorials(GetTutorialsBody getTutorialsBody)
        {
            throw new NotImplementedException();
        }
    }
}
