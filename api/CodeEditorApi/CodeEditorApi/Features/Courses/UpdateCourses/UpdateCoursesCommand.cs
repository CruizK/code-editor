using CodeEditorApi.Errors;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.UpdateCourses
{
    public interface IUpdateCoursesCommand
    {
        public Task<ActionResult<Course>> ExecuteAsync(int courseId, int userId, UpdateCourseBody updateCourseBody);
    }

    public class UpdateCoursesCommand : IUpdateCoursesCommand
    {

        private readonly IUpdateCourses _updateCourses;
        private readonly IGetCourses _getCourses;

        public UpdateCoursesCommand(IUpdateCourses updateCourses, IGetCourses getCourses)
        {
            _updateCourses = updateCourses;
            _getCourses = getCourses;
        }

        public async Task<ActionResult<Course>> ExecuteAsync(int courseId, int userId, UpdateCourseBody updateCourseBody)
        {
            var createdCourses = await _getCourses.GetUserCreatedCourses(userId);

            if (!createdCourses.Value.Select(x => x.Id).Contains(courseId))
            {
                return ApiError.BadRequest("Only the author of a course may edit it");
            }

            return await _updateCourses.ExecuteAsync(courseId, updateCourseBody);
        }
    }
}
