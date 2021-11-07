using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.UpdateCourses
{
    public interface IUpdateCourses
    {
        public Task<ActionResult<Course>> ExecuteAsync(int courseId, UpdateCourseBody updateCourseBody);
    }
    public class UpdateCourses : IUpdateCourses
    {
        private readonly CodeEditorApiContext _context;

        public UpdateCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Course>> ExecuteAsync(int courseId, UpdateCourseBody updateCourseBody)
        {
            var existingCourse = await _context.Courses.FindAsync(courseId);

            if(existingCourse != null)
            {
                existingCourse.Title = updateCourseBody.Title ?? existingCourse.Title;
                existingCourse.Description = updateCourseBody.Description ?? existingCourse.Description;
                existingCourse.IsPublished = updateCourseBody.IsPublished;
                existingCourse.ModifyDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return existingCourse;
        }
    }
}
