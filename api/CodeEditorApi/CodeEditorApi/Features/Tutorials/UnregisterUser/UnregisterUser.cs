using CodeEditorApiDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.UnregisterUser
{
    public interface IUnregisterUser
    {
        public Task<List<UserTutorial>> ExecuteAsync(int courseId, int userId);
    }
    public class UnregisterUser : IUnregisterUser
    {
        private readonly CodeEditorApiContext _context;
        public UnregisterUser(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<List<UserTutorial>> ExecuteAsync(int courseId, int userId)
        {
            var tutorialIds = await _context.Tutorials.Where(t => t.CourseId == courseId)
                .Select(t => t.Id).ToListAsync();

            var userTutorials = await _context.UserTutorials.Where(ut =>
                tutorialIds.Contains(ut.TutorialId) && (ut.UserId == userId)).Select(ut => ut).ToListAsync();

            if(userTutorials != null)
            {
                _context.UserTutorials.RemoveRange(userTutorials);
                await _context.SaveChangesAsync();
            }

            return userTutorials;
        }
    }
}
