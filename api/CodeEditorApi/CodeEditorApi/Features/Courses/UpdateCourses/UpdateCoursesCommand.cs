using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.UpdateCourses
{
    public interface IUpdateCoursesCommand
    {
        public Task<Course> ExecuteAsync(Course course);
    }

    public class UpdateCoursesCommand : IUpdateCoursesCommand
    {

        private readonly IUpdateCourses _updateCourses;

        public UpdateCoursesCommand(IUpdateCourses updateCourses)
        {
            _updateCourses = updateCourses;
        }

        public async Task<Course> ExecuteAsync(Course course)
        {
            return await _updateCourses.ExecuteAsync(course);

            // If we needed to manipulate the data, or do some data validation it would be here
        }
    }
}
