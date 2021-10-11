using CodeEditorApiDataAccess.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface IGetCoursesCommand
    {
        public Task<IEnumerable<Course>> ExecuteAsync(int userId);
    }

    public class GetCoursesCommand : IGetCoursesCommand
    {

        private readonly IGetCourses _getCourses;

        public GetCoursesCommand(IGetCourses getCourses)
        {
            _getCourses = getCourses;
        }

        public async Task<IEnumerable<Course>> ExecuteAsync(int userId)
        {
            var courses = await _getCourses.ExecuteAsync(userId);

            // If we needed to manipulate the data, or do some data validation it would be here

            return courses;
        }
    }
}
