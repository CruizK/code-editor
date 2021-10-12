using CodeEditorApiDataAccess.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace CodeEditorApi.Features.Courses.GetCourses
{

    public interface IGetCourses
    {
        public Task<IEnumerable<Course>> ExecuteAsync(int userId);
    }
    public class GetCourses : IGetCourses
    {

        private readonly CodeEditorApiContext _context;

        public GetCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> ExecuteAsync(int userId)
        {
            var userCourses = await _context.UserRegisteredCourses.Where(urc => urc.UserId == userId).Select(urc => urc.CourseId).ToListAsync();

            var courseList = await _context.Courses.Where(c => userCourses.Contains(c.Id)).ToListAsync();

            return courseList;

        }
    }
}
