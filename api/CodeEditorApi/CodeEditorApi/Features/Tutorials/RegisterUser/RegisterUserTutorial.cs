using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.RegisterUser
{
    public interface IRegisterUserTutorial
    {
        public Task<List<Tutorial>> ExecuteAsync(int userId, List<Tutorial> tutorials);
    }
    public class RegisterUserTutorial : IRegisterUserTutorial
    {

        private readonly CodeEditorApiContext _context;
        public RegisterUserTutorial(CodeEditorApiContext context)
        {
            _context = context;
        }
        public async Task<List<Tutorial>> ExecuteAsync(int userId, List<Tutorial> tutorials)
        {
            foreach(var tut in tutorials)
            {
                await _context.UserTutorials.AddAsync(new UserTutorial { UserId = userId, TutorialId = tut.Id, InProgress = false, IsCompleted = false });
            }

            await _context.SaveChangesAsync();
            return tutorials;
        }
    }
}
