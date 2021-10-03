using CodeEditorApiDataAccess.Data;
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
    public class GetCourse : IGetCourse
    {

        private readonly CodeEditorApiContext _context;

        public GetCourse(CodeEditorApiContext context)
        {
            _context = context;
        }

        [HttpGet("User Courses")]
        [Authorize]
        public Task<IEnumerable<Course>> ExecuteAsync(int userId)
        {
            var userCourses = _context.UserRegisteredCourses.Where(urc => urc.UserId == userId).Select(urc => urc.CourseId).ToList();

            var courseList = _context.Courses.Where(c => userCourses.Contains(c.Id));
            
            return (Task<IEnumerable<Course>>)courseList;
            
        }
    }
}
