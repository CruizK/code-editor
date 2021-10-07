using CodeEditorApiDataAccess.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface IGetCourseCommand
    {
        public Task<IEnumerable<Course>> ExecuteAsync(int userId);
    }

    public class GetCoursesCommand : IGetCourseCommand
    {

        private readonly IGetCourse _getCourse;

        public GetCoursesCommand(IGetCourse getCourse)
        {
            _getCourse = getCourse;
        }

        public async Task<IEnumerable<Course>> ExecuteAsync(int userId)
        {
            var courses = await _getCourse.ExecuteAsync(userId);

            // If we needed to manipulate the data, or do some data validation it would be here

            return courses;
        }
    }
}
