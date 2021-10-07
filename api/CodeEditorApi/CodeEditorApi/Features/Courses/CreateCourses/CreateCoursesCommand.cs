using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.CreateCourses
{
    public interface ICreateCourseCommand
    {
        public Task ExecuteAsync(int userId, Course course);
    }
    public class CreateCoursesCommand : ICreateCourseCommand
    {
        private readonly ICreateCourse _createCourse;

        public CreateCoursesCommand(ICreateCourse createCourse)
        {
            _createCourse = createCourse;
        }
        public Task ExecuteAsync(int userId, Course course)
        {
            return _createCourse.ExecuteAsync(userId, course);
        }
    }
}
