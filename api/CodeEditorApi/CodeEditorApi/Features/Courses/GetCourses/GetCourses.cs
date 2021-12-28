using CodeEditorApiDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{

    public interface IGetCourses
    {
        public Task<List<Course>> GetUserCourses(int userId);

        public Task<List<Course>> GetUserCreatedCourses(int userId);

        public Task<Course> GetCourseDetails(int courseId);

        public Task<List<Course>> GetAllPublishedCourses();

        public Task<List<Course>> GetAllPublishedCoursesSortByModifyDate();
    }
    public class GetCourses : IGetCourses
    {

        private readonly CodeEditorApiContext _context;

        public GetCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetUserCourses(int userId)
        {
            var courseList = await _context.UserRegisteredCourses.Where(urc => urc.UserId == userId).Select(urc => urc.Course).ToListAsync();

            return courseList;
        }

        public async Task<List<Course>> GetUserCreatedCourses(int userId)
        {
            return await _context.Courses.Where(c => c.Author == userId).Select(c => c).ToListAsync();
        }

        public async Task<Course> GetCourseDetails(int courseId)
        {
            var course = await _context.Courses
                .Where(c => c.Id.Equals(courseId))
                .Include(c => c.Tutorials) // the 'include' loads all tutorials related to the course
                    .ThenInclude(t => t.Difficulty) // the 'theninclude' then loads the difficulty of a given tutorial as well as all tutorials under that difficulty that have ALREADY been loaded
                .Include(c => c.Tutorials)
                    .ThenInclude(t => t.Language)
                .FirstOrDefaultAsync();
            return course;
        }

        public async Task<List<Course>> GetAllPublishedCourses()
        {
            return await _context.Courses
                .Where(c => c.IsPublished == true)
                .Select(c => new Course{
                    Id = c.Id,
                    Title = c.Title,
                    Author = c.Author
                }).ToListAsync();
        }

        public async Task<List<Course>> GetAllPublishedCoursesSortByModifyDate()
        {
            return await _context.Courses
                .Where(c => c.IsPublished == true)
                .OrderByDescending(c => c.ModifyDate)
                .Select(c => new Course
                {
                    Id = c.Id,
                    Title = c.Title,
                    Author = c.Author
                }).ToListAsync();
        }
    }
}
