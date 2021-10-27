using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.DeleteCourses
{

    public interface IDeleteCourses
    {
        public Task<Course> ExecuteAsync(Course course);
    }
    public class DeleteCourses : IDeleteCourses
    {
        private readonly CodeEditorApiContext _context;

        public DeleteCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<Course> ExecuteAsync(Course course)
        {
            var existingCourse = await _context.Courses.FindAsync(course.Id);
            if (existingCourse != null)
            {
                _context.Courses.Remove(existingCourse);
                await _context.SaveChangesAsync();
            }
            return existingCourse;
        }
    }
}
