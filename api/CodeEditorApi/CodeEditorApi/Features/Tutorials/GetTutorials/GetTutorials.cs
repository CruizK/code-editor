using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetTutorials
    {
        public Task<Tutorial> GetUserTutorials(GetTutorialsBody getTutorialsBody);

        public Task<IEnumerable<Tutorial>> GetUserCreatedTutorials(GetTutorialsBody getTutorialsBody);
    }
    public class GetTutorials : IGetTutorials
    {
        private readonly CodeEditorApiContext _context;

        public GetTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Tutorial>> GetUserCreatedTutorials(GetTutorialsBody getTutorialsBody)
        {
            throw new NotImplementedException();
        }

        public async Task<Tutorial> GetUserTutorials(GetTutorialsBody getTutorialsBody)
        {
            var tutorial = await _context.Tutorials.FindAsync(getTutorialsBody.TutorialId);

            if (tutorial == null) throw new Exception($"There is no Tutorial that exists with Id {getTutorialsBody.TutorialId}");
            else return tutorial;
        }
    }
}
