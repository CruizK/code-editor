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

        public Task<List<Course>> SearchCourses(SearchInput si);
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
                .Where(c => c.IsPublished)
                .ToListAsync();
        }

        public async Task<List<Course>> GetAllPublishedCoursesSortByModifyDate()
        {
            return await _context.Courses
                .Where(c => c.IsPublished)
                .OrderByDescending(c => c.ModifyDate)
                .ToListAsync();
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

            var courses = await _context.Courses.Where(c => c.IsPublished && mostPopularCourseIds.Contains(c.Id)).ToListAsync();

            return courses;

        }

        public async Task<List<Course>> SearchCourses(SearchInput si)
        {
            var LID = si.languageId;
            var DID = si.difficultyId;
            var courseIds = new List<int>();

            //if filtering by both Language and Difficulty
            if (LID > 0 && DID > 0)
            {
                courseIds = await _context.Tutorials
                                    .Where(t => t.IsPublished && 
                                        (t.Title.ToLower().Contains(si.searchString.ToLower())
                                        || t.LanguageId == si.languageId
                                        || t.DifficultyId == si.difficultyId))
                                    .Select(t => t.CourseId).Distinct()
                                    .ToListAsync();
            }//if filtering by only Language
            else if (LID > 0 && DID == 0)
            {
                courseIds = await _context.Tutorials
                                    .Where(t => t.IsPublished && 
                                        (t.Title.ToLower().Contains(si.searchString.ToLower())
                                        || t.LanguageId == si.languageId))
                                    .Select(t => t.CourseId).Distinct()
                                    .ToListAsync();
            }//if filtering by only Difficulty
            else if (LID == 0 && DID > 0)
            {
                courseIds = await _context.Tutorials
                                    .Where(t => t.IsPublished && 
                                        (t.Title.ToLower().Contains(si.searchString.ToLower())
                                        || t.DifficultyId == si.difficultyId))
                                    .Select(t => t.CourseId).Distinct()
                                    .ToListAsync();
            }//if not filtering by either Language or Difficulty
            else {
                courseIds = await _context.Tutorials
                                    .Where(t => t.IsPublished && 
                                        (t.Title.ToLower().Contains(si.searchString.ToLower())))                   
                                    .Select(t => t.CourseId).Distinct()
                                    .ToListAsync();
            }           

            var courseDetails = await _context.Courses
                                    .Where(c => c.IsPublished && 
                                                (c.Tutorials.Where(t => t.IsPublished).Count() > 0) && 
                                                (c.Title.Contains(si.searchString) 
                                                || courseIds.Contains(c.Id)))
                                    .ToListAsync();

            

            return courseDetails; //needs pagination
            
        }
    }
}
