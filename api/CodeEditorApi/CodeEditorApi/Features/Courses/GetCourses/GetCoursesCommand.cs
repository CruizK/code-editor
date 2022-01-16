using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface IGetCoursesCommand
    {
        public Task<ActionResult<List<Course>>> ExecuteAsync(int userId);
    }

    public class GetCoursesCommand : IGetCoursesCommand
    {

        private readonly IGetCourses _getCourses;

        public GetCoursesCommand(IGetCourses getCourses)
        {
            _getCourses = getCourses;
        }

        public async Task<ActionResult<List<Course>>> ExecuteAsync(int userId)
        {
            var courses = await _getCourses.GetUserCourses(userId);

            return courses;
        }
    }
}
