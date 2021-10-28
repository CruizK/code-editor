using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetTutorials
    {
        public Task<ActionResult<Tutorial>> GetUserTutorials(int tutorialId);

        public Task<ActionResult<List<Tutorial>>> GetUserCreatedTutorials(int userId);

        public Task<ActionResult<List<Tutorial>>> GetCourseTutorials(int courseId);
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
            var tutorial = await _context.Tutorials.FindAsync(tutorialId);

            if (tutorial == null) throw new Exception($"There is no Tutorial that exists with Id {tutorialId}");
            else return tutorial;
        }

        public async Task<ActionResult<List<Tutorial>>> GetUserCreatedTutorials(int userId)
        {
            return await _context.Tutorials.Where(t => t.Author == userId).Select(t => t).ToListAsync();
        }

        public async Task<ActionResult<List<Tutorial>>> GetCourseTutorials(int courseId)
        {
            return await _context.Tutorials.Where(t => t.CourseId == courseId).Select(t => t).ToListAsync();
        }
    }
}
