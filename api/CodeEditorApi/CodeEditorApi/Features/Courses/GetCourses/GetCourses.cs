using CodeEditorApiDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CodeEditorApi.Features.Courses.GetCourses.GetCourses;

namespace CodeEditorApi.Features.Courses.GetCourses
{

    public interface IGetCourses
    {
        public Task<List<Course>> GetUserCourses(int userId);

        public Task<List<Course>> GetUserCreatedCourses(int userId);

        public Task<Course> GetCourseDetails(int courseId);

        public Task<List<SearchCoursesResponse>> GetAllPublishedCourses();

        public Task<List<Course>> GetAllPublishedCoursesSortByModifyDate();

        public Task<List<Course>> GetMostPopularCourses();

        public Task<List<SearchCoursesResponse>> SearchCourses(SearchInput si);
    }
    public class GetCourses : IGetCourses
    {

        private readonly CodeEditorApiContext _context;

        public GetCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        //get the courses that a user is registered to
        public async Task<List<Course>> GetUserCourses(int userId)
        {
            var courseList = await _context.UserRegisteredCourses.Where(urc => urc.UserId == userId).Select(urc => urc.Course).ToListAsync();

            return courseList;
        }

        //get courses created by a teacher
        public async Task<List<Course>> GetUserCreatedCourses(int userId)
        {
            return await _context.Courses.Where(c => c.Author == userId).Select(c => c).ToListAsync();
        }

        public async Task<Course> GetCourseDetails(int courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }

        public async Task<List<SearchCoursesResponse>> GetAllPublishedCourses()
        {
            return await _context.Courses
                .Where(c => c.IsPublished)
                .Select(c => new SearchCoursesResponse
                {
                    courseId = c.Id,
                    courseTitle = c.Title
                })
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

        //public async Task<List<Course>> SearchCourses2(SearchInput si)
        //{
        //    var courseIds = new List<int>();

        //    //if filtering by both Language and Difficulty
        //    if (LID > 0 && DID > 0)
        //    {
        //        courseIds = await _context.Tutorials
        //                            .Where(t => t.IsPublished &&
        //                                (t.Title.ToLower().Contains(si.searchString.ToLower())
        //                                || t.LanguageId == si.languageId
        //                                || t.DifficultyId == si.difficultyId))
        //                            .Select(t => t.CourseId).Distinct()
        //                            .ToListAsync();
        //    }//if filtering by only Language
        //    else if (LID > 0 && DID == 0)
        //    {
        //        courseIds = await _context.Tutorials
        //                            .Where(t => t.IsPublished &&
        //                                (t.Title.ToLower().Contains(si.searchString.ToLower())
        //                                || t.LanguageId == si.languageId))
        //                            .Select(t => t.CourseId).Distinct()
        //                            .ToListAsync();
        //    }//if filtering by only Difficulty
        //    else if (LID == 0 && DID > 0)
        //    {
        //        courseIds = await _context.Tutorials
        //                            .Where(t => t.IsPublished &&
        //                                (t.Title.ToLower().Contains(si.searchString.ToLower())
        //                                || t.DifficultyId == si.difficultyId))
        //                            .Select(t => t.CourseId).Distinct()
        //                            .ToListAsync();
        //    }//if not filtering by either Language or Difficulty
        //    else
        //    {
        //        courseIds = await _context.Tutorials
        //                            .Where(t => t.IsPublished &&
        //                                (t.Title.ToLower().Contains(si.searchString.ToLower())))
        //                            .Select(t => t.CourseId).Distinct()
        //                            .ToListAsync();
        //    }

        //}

        public async Task<List<SearchCoursesResponse>> SearchCourses(SearchInput si)
        {
            var courseTutorial = await _context.Courses.Join(
                                        _context.Tutorials,
                                        c => c.Id,
                                        t => t.CourseId,
                                        (c, t) => new CourseTutorial
                                        {
                                            cId = c.Id,
                                            cTitle = c.Title,
                                            cPub = c.IsPublished,
                                            tTitle = t.Title,
                                            tPub = t.IsPublished,
                                            tLId = t.LanguageId,
                                            tDId = t.DifficultyId
                                        }
                                        ).Where(ct => ct.cPub && ct.tPub).ToListAsync();

            var courseDetails = new List<SearchCoursesResponse>();
            
            //if at least the search string isn't empty, then query with all three criteria (diff/lang ids of 0 just won't return anything)
            if (si.searchString != null)
            {
                courseDetails = courseTutorial.Where(ct => ct.tPub
                            && (ct.tLId == si.languageId
                                || ct.tDId == si.difficultyId
                                || ct.cTitle.Contains(si.searchString)
                                || ct.tTitle.Contains(si.searchString)                                
                                )
                            )
                        .OrderByDescending(ct => ct.tLId == si.languageId)
                        .ThenByDescending(ct => ct.tDId == si.difficultyId)
                        .ThenByDescending(ct => ct.cTitle.Contains(si.searchString))
                        .ThenByDescending(ct => ct.tTitle.Contains(si.searchString))
                        .Select(ct => new SearchCoursesResponse {
                            courseId = ct.cId,
                            courseTitle = ct.cTitle
                        })
                        .ToList();
            }
            // else if the search string is empty, but at least a diff/lang has been chosen, then search only for those 2 criteria
            // (passing null search string would return bad request error)
            else if (si.languageId > 0 || si.difficultyId > 0)
            {
                courseDetails = courseTutorial.Where(ct => ct.tPub
                            && (ct.tLId == si.languageId
                                || ct.tDId == si.difficultyId))
                        .OrderByDescending(ct => ct.tLId == si.languageId)
                        .ThenByDescending(ct => ct.tDId == si.difficultyId)
                        .Select(ct => new SearchCoursesResponse
                        {
                            courseId = ct.cId,
                            courseTitle = ct.cTitle
                        })
                        .Distinct()
                        .ToList();
            }

            //Distinct() on the object SearchCoursesResponse
            courseDetails = courseDetails.GroupBy(scr => scr.courseId).Select(scr => scr.First()).ToList();
            return courseDetails;
           
        }

        private class CourseTutorial
        {
            public int cId { get; set; }
            public string cTitle { get; set; }
            public bool cPub { get; set; }
            public string tTitle { get; set; }
            public bool tPub { get; set; }
            public int? tLId { get; set; }
            public int? tDId { get; set; }
        }
        
        public class SearchCoursesResponse
        {
            public int courseId { get; set; }
            public string courseTitle { get; set; }
        }
    }
}
