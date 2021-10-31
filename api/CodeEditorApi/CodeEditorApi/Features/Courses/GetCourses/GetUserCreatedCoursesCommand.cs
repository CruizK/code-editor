using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface IGetUserCreatedCoursesCommand
    {
        public Task<ActionResult<List<Course>>> ExecuteAsync(int userId);
    }
    public class GetUserCreatedCoursesCommand : IGetUserCreatedCoursesCommand
    {
        private readonly IGetCourses _getCourses;

        public GetUserCreatedCoursesCommand(IGetCourses getCourses)
        {
            _getCourses = getCourses;
        }
        public async Task<ActionResult<List<Course>>> ExecuteAsync(int userId)
        {
            var courses = await _getCourses.GetUserCreatedCourses(userId);
            return courses;
        }
    }
}
