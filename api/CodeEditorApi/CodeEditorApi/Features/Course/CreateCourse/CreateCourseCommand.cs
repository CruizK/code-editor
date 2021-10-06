using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.CreateCourse
{
    public interface ICreateCourseCommand
    {
        public Task ExecuteAsync(int userId, Course course);
    }
    public class CreateCourseCommand : ICreateCourseCommand
    {
        private readonly ICreateCourse _createCourse;

        public CreateCourseCommand(ICreateCourse createCourse)
        {
            _createCourse = createCourse;
        }
        public Task ExecuteAsync(int userId, Course course)
        {
            return _createCourse.ExecuteAsync(userId, course);
        }
    }
}
