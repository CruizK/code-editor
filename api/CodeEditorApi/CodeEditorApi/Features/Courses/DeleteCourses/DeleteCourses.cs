using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.DeleteCourses
{

    public interface IDeleteCourses
    {
        public Task<ActionResult<Course>> ExecuteAsync(int courseId);
    }
    public class DeleteCourses : IDeleteCourses
    {
        private readonly CodeEditorApiContext _context;

        public DeleteCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Course>> ExecuteAsync(int courseId)
        {
            var existingCourse = await _context.Courses.FindAsync(courseId);
            if (existingCourse != null)
            {
                _context.Courses.Remove(existingCourse);
                await _context.SaveChangesAsync();
            }
            return existingCourse;
        }
    }
}
