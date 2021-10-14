using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.UpdateCourses
{
    public interface IUpdateCourses
    {
        public Task<Course> ExecuteAsync(Course course);
    }
    public class UpdateCourses : IUpdateCourses
    {
        private readonly CodeEditorApiContext _context;

        public UpdateCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<Course> ExecuteAsync(Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(course.Id);
            if(existingCourse != null)
            {
                existingCourse.Title = course.Title;
                existingCourse.Description = course.Description;
                existingCourse.ModifyDate = DateTime.Now;
                existingCourse.IsPublished = course.IsPublished;
            }
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return existingCourse;
        }
    }
}
