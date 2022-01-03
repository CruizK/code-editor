using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodeEditorApi.Errors;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetTutorials
    {
        public Task<ActionResult<Tutorial>> GetUserTutorials(int tutorialId);

        public Task<ActionResult<List<Tutorial>>> GetUserCreatedTutorials(int userId);

        public Task<List<Tutorial>> GetCourseTutorials(int courseId);

        public Task<Tutorial> GetUserLastInProgressTutorial(int userId, int courseId);
        public Task<List<UserTutorial>> GetUserRegisteredTutorials(int userId);
    }
    public class GetTutorials : IGetTutorials
    {
        private readonly CodeEditorApiContext _context;

        public GetTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Tutorial>> GetUserTutorials(int tutorialId)
        {
            var tutorial = await _context.Tutorials.Where(t => t.Id == tutorialId).Select(t => t).FirstOrDefaultAsync();
            
            return tutorial;
        }

        public async Task<ActionResult<List<Tutorial>>> GetUserCreatedTutorials(int userId)
        {
            return await _context.Tutorials.Where(t => t.Author == userId).Select(t => t).ToListAsync();
        }

        public async Task<List<Tutorial>> GetCourseTutorials(int courseId)
        {
            return await _context.Tutorials.Where(t => t.CourseId == courseId)
                .Include(c => c.Difficulty)
                .Include(c => c.Language)
                .Select(t => t).ToListAsync();
        }

        public async Task<List<UserTutorial>> GetUserRegisteredTutorials(int userId)
        {
            return await _context.UserTutorials.Where(t => t.UserId == userId).Select(t => t).ToListAsync();
        }

        public async Task<Tutorial> GetUserLastInProgressTutorial(int userId, int courseId)
        {
            /*var userIsRegisteredForCourse = await _context.UserRegisteredCourses.Where(urc => urc.UserId == userId)
                   .Select(urc => urc).AnyAsync();
            if (!userIsRegisteredForCourse) {
                return ApiError.BadRequest($"User is not registered for course with id {courseId}");
            }
*/
            var courseTutorials = await _context.Tutorials.Where(t => t.CourseId == courseId).Select(t => t.Id).ToListAsync();
            var userTutorials = await _context.UserTutorials.Where(ut => courseTutorials.Contains(ut.TutorialId) && ut.UserId == userId).Select(o => o).ToListAsync();

            var inProgressTutorialId = userTutorials.Where(ut => ut.InProgress == true).Select(ut => ut.TutorialId).FirstOrDefault();


            /*if (userTutorialInProgress.Count() == 0)
            {
                return ApiError.BadRequest($"User has not started any tutorial for course with id {courseId}");
            }*/

            var tutorial = await _context.Tutorials.Where(t => t.Id == inProgressTutorialId).Select(t => t).FirstOrDefaultAsync();

            return tutorial;
        }

    }
}
