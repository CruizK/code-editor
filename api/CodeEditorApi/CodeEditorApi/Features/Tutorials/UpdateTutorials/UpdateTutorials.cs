using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApiDataAccess.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.UpdateTutorials
{
    public interface IUpdateTutorials
    {
        public Task<Tutorial> UpdateTutorial(int tutorialId, CreateTutorialsBody createTutorialsBody);

        public Task<UserTutorial> UpdateUserTutorial(int tutorialId, int userId, UpdateUserTutorialBody updateUserTutorialBody);
    }
    public class UpdateTutorials : IUpdateTutorials
    {
        private readonly CodeEditorApiContext _context;

        public UpdateTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }
        public async Task<Tutorial> UpdateTutorial(int tutorialId, CreateTutorialsBody createTutorialsBody)
        {
            var existingTutorial = await _context.Tutorials.FindAsync(tutorialId);
            if (existingTutorial != null)
            {
                _context.Entry(existingTutorial).CurrentValues.SetValues(createTutorialsBody);
                await _context.SaveChangesAsync();
            }

            return await _context.Tutorials.FindAsync(tutorialId);
        }

        public async Task<UserTutorial> UpdateUserTutorial(int tutorialId, int userId, UpdateUserTutorialBody updateUserTutorialBody)
        {
            var existingUserTutorial = _context.UserTutorials.Where(ut => (ut.TutorialId == tutorialId)
                    && (ut.UserId == userId)).FirstOrDefault();

            if (existingUserTutorial != null)
            {
                existingUserTutorial.InProgress = updateUserTutorialBody.InProgress;
                existingUserTutorial.IsCompleted = updateUserTutorialBody.IsCompleted;
                existingUserTutorial.UserCode = updateUserTutorialBody.UserCode;
                existingUserTutorial.ModifyDate = DateTime.Now;

            }
            await _context.SaveChangesAsync();

            var updatedUserTutorial =  _context.UserTutorials.Where(ut => (ut.TutorialId == tutorialId)
                    && (ut.UserId == userId)).FirstOrDefault();

            return updatedUserTutorial;
        }
    }
}
