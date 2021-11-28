using CodeEditorApi.Errors;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface IGetAllPublishedCoursesCommand
    {
        public Task<ActionResult<List<Course>>> GetAllPublishedCourses();
        public Task<ActionResult<List<Course>>> GetAllPublishedCoursesSortByModifyDate();
    }
    public class GetAllPublishedCoursesCommand : IGetAllPublishedCoursesCommand
    {
        private readonly IGetCourses _getCourses;
        
        public GetAllPublishedCoursesCommand(IGetCourses getCourses)
        {
            _getCourses = getCourses;
        }
        public async Task<ActionResult<List<Course>>> GetAllPublishedCourses()
        {
            var courses = await _getCourses.GetAllPublishedCourses();

            if (courses.Count() < 1) return ApiError.BadRequest("No published courses found");

            return courses;
        }
        public async Task<ActionResult<List<Course>>> GetAllPublishedCoursesSortByModifyDate()
        {
            var courses = await _getCourses.GetAllPublishedCoursesSortByModifyDate();

            if (courses.Count() < 1) return ApiError.BadRequest("No published courses found");

            return courses;
        }
    }
}
