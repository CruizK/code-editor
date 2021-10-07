using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.DeleteCourses
{
    public interface IDeleteCoursesCommand
    {
        public Task<Course> ExecuteAsync(Course course);
    }
    public class DeleteCoursesCommand : IDeleteCoursesCommand
    {
        private readonly IDeleteCourses _deleteCourses;

        public DeleteCoursesCommand(IDeleteCourses deleteCourses)
        {
            _deleteCourses = deleteCourses;
        }

        public async Task<Course> ExecuteAsync(Course course)
        {
            return await _deleteCourses.ExecuteAsync(course);

            // If we needed to manipulate the data, or do some data validation it would be here
        }
    }
}
