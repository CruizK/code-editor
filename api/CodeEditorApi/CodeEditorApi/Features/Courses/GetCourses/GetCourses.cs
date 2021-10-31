using CodeEditorApiDataAccess.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace CodeEditorApi.Features.Courses.GetCourses
{

    public interface IGetCourses
    {
        public Task<IEnumerable<Course>> GetUserCourses(int userId);

        public Task<IEnumerable<Course>> GetUserCreatedCourses(int userId);
    }
    public class GetCourses : IGetCourses
    {

        private readonly CodeEditorApiContext _context;

        public GetCourses(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetUserCourses(int userId)
        {
            var userCourses = await _context.UserRegisteredCourses.Where(urc => urc.UserId == userId).Select(urc => urc.CourseId).ToListAsync();

            var courseList = await _context.Courses.Where(c => userCourses.Contains(c.Id)).ToListAsync();

            return courseList;

        }

        public async Task<IEnumerable<Course>> GetUserCreatedCourses(int userId)
        {
            var userCreatedCourses = await _context.Courses.Where(c => c.Author == userId).Select(c => c).ToListAsync();

            return userCreatedCourses;
        }
    }
}
