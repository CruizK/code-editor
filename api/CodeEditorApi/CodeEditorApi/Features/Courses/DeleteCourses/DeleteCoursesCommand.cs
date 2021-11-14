using CodeEditorApi.Errors;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.DeleteCourses
{
    public interface IDeleteCoursesCommand
    {
        public Task<ActionResult<Course>> ExecuteAsync(int userId, int courseId);
    }
    public class DeleteCoursesCommand : IDeleteCoursesCommand
    {
        private readonly IDeleteCourses _deleteCourses;
        private readonly IGetCourses _getCourses;

        public DeleteCoursesCommand(IDeleteCourses deleteCourses, IGetCourses getCourses)
        {
            _deleteCourses = deleteCourses;
            _getCourses = getCourses;
        }

        public async Task<ActionResult<Course>> ExecuteAsync(int userId, int courseId)
        {
            var createdCourses = await _getCourses.GetUserCreatedCourses(userId);

            if (createdCourses.Count() == 0 || !createdCourses.Select(c => c.Id).Contains(courseId))
            {
                return ApiError.BadRequest("Only the author of a course may delete it");
            }

            return await _deleteCourses.ExecuteAsync(courseId);
        }
    }
}
