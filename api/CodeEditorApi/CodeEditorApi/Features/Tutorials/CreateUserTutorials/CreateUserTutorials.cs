using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.CreateUserTutorials
{
    public interface ICreateUserTutorials
    {
        Task<int> ExecuteAsync(int TutorialId, List<int> UserId);
    }
    public class CreateUserTutorials : ICreateUserTutorials
    {
        private readonly CodeEditorApiContext _context;
        public CreateUserTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }
        public async Task<int> ExecuteAsync(int TutorialId, List<int> userIds)
        {
            foreach(int id in userIds)
            {
                var userTutorial = new UserTutorial()
                {
                    TutorialId = TutorialId,
                    UserId = id,
                    InProgress = false,
                    IsCompleted = false
                };
                _context.UserTutorials.Add(userTutorial);
            }

            return await _context.SaveChangesAsync();
        }
    }
}
