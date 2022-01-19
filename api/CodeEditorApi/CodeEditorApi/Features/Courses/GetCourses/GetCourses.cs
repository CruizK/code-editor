using CodeEditorApiDataAccess.Data;
using Microsoft.EntityFrameworkCore;
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

        public Task<List<Course>> GetMostPopularCourses();
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
            return await _context.Courses.FindAsync(courseId);
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

        public async Task<List<Course>> GetMostPopularCourses()
        {
            int top = 10;            

            var result = await _context.UserRegisteredCourses
                .GroupBy(c => c.CourseId)
                .OrderByDescending(c => c.Count())
                .ThenBy(c => c.Key)
                .Select(c => c.Key).ToListAsync();

            var mostPopularCourseIds = result.Take(top).ToList();

            var courses = await _context.Courses.Where(c => mostPopularCourseIds.Contains(c.Id)).ToListAsync();

            return courses;

        }
    }
}
