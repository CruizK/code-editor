using CodeEditorApi.Errors;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface IGetMostPopularCoursesCommand
    {
        public Task<ActionResult<List<Course>>> ExecuteAsync();
    }
    public class GetMostPopularCoursesCommand : IGetMostPopularCoursesCommand
    {
        private readonly IGetCourses _getCourses;
        public GetMostPopularCoursesCommand(IGetCourses getCourses)
        {
            _getCourses = getCourses;
        }
        public async Task<ActionResult<List<Course>>> ExecuteAsync()
        {
            if (_getCourses.GetAllPublishedCourses() == null)
            {
                return ApiError.BadRequest($"No courses have been published.");
            }
            return await _getCourses.GetMostPopularCourses();
        }
    }
}
