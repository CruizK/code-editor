using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.CreateCourses
{
    public interface ICreateCoursesCommand
    {
        public Task<ActionResult<Course>> ExecuteAsync(int userId, CreateCourseBody createCourseBody);
    }
    public class CreateCoursesCommand : ICreateCoursesCommand
    {
        private readonly ICreateCourses _createCourses;

        public CreateCoursesCommand(ICreateCourses createCourses)
        {
            _createCourses = createCourses;
        }
        public async Task<ActionResult<Course>> ExecuteAsync(int userId, CreateCourseBody createCourseBody)
        {
            var course = new Course()
            {
                Title = createCourseBody.Title,
                Author = userId,
                Description = createCourseBody.Description,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                IsPublished = createCourseBody.IsPublished
            };

            var createdCourse = await _createCourses.ExecuteAsync(course);

            return createdCourse;
        }
    }
}
