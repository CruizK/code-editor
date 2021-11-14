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
    }
}
