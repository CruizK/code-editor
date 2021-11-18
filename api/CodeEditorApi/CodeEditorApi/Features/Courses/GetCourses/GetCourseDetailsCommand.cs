using CodeEditorApi.Errors;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface IGetCourseDetailsCommand
    {
        public Task<ActionResult<Course>> ExecuteAsync(int courseId);
    }
    public class GetCourseDetailsCommand : IGetCourseDetailsCommand
    {
        private readonly IGetCourses _getCourses;

        public GetCourseDetailsCommand(IGetCourses getCourses)
        {
            _getCourses = getCourses;
        }
        public async Task<ActionResult<Course>> ExecuteAsync(int courseId)
        {
            var course = await _getCourses.GetCourseDetails(courseId);

            if(course == null)
            {
                return ApiError.BadRequest($"Could not find course with ID {courseId}");
            }

            return course;
        }
    }
}
