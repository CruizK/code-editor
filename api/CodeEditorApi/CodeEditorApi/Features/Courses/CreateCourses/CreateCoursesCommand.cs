using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.CreateCourses
{
    public interface ICreateCoursesCommand
    {
        public Task ExecuteAsync(int userId, Course course);
    }
    public class CreateCoursesCommand : ICreateCoursesCommand
    {
        private readonly ICreateCourses _createCourses;

        public CreateCoursesCommand(ICreateCourses createCourses)
        {
            _createCourses = createCourses;
        }
        public Task ExecuteAsync(int userId, Course course)
        {
            return _createCourses.ExecuteAsync(userId, course);
        }
    }
}
