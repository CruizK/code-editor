using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.CreateCourses
{
    public interface ICreateCourses
    {
        public Task ExecuteAsync(int userId, Course course);
    }
    public class CreateCourses : ICreateCourses
    {
        private readonly CodeEditorApiContext _context;

        public CreateCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(int userId, Course course)
        {
            await _context.Courses.AddAsync(course);
        }
    }
}
