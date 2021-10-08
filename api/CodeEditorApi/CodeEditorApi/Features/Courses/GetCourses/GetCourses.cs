using CodeEditorApiDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;

namespace CodeEditorApi.Features.Courses.GetCourses
{

    public interface IGetCourse
    {
        public Task<IEnumerable<Course>> ExecuteAsync(int userId);
    }
    public class GetCourses : IGetCourse
    {

        private readonly CodeEditorApiContext _context;

        public GetCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> ExecuteAsync(int userId)
        {
            var userCourses = _context.UserRegisteredCourses.Where(urc => urc.UserId == userId).Select(urc => urc.CourseId);

            var courseList = await _context.Courses.Where(c => userCourses.Contains(c.Id)).ToListAsync();

            return courseList;

        }
    }
}
