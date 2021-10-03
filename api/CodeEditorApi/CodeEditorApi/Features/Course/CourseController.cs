using CodeEditorApi.Features.Courses.CreateCourse;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace CodeEditorApi.Features.Courses
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]

    /// <summary>
    /// Controls the direction of which CRUD operation/API request is called
    /// </summary>
    public class CourseController : ControllerBase
    {
        private readonly IGetCourseCommand _getCourseCommand;
        private readonly ICreateCourseCommand _createCourseCommand;

        public CourseController(IGetCourseCommand getCourseCommand, ICreateCourseCommand createCourseCommand)
        {
            _getCourseCommand = getCourseCommand;
            _createCourseCommand = createCourseCommand;
        }

        /// <summary>
        /// Get's all courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Course>> GetCourses()
        {
            var userId = retrieveRequestUserId();
            return await _getCourseCommand.ExecuteAsync(userId);
        }

        public async Task<IEnumerable<Course>> CreateCourse()
        {
            var userId = retrieveRequestUserId();
            return await _createCourseCommand.ExecuteAsync(userId);
        }

        private int retrieveRequestUserId()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
            try
            {
                return int.Parse(userId);
            }
            catch(System.FormatException e)
            {
                return -1;
                //TODO: catch internal error of invalid userId...this should turn into a validation on it's own though. Then call validation in this method.
            }
        }
        //[HttpGet("all")]
        //[Authorize(Roles = "Student")]
        //public async Task<IEnumerable<Course>> GetAllCourses()
        //{
        //    var user = HttpContext.User;
        //    // This is how you would get the id, or any other information stored in the JWT
        //    var userId = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

        //    // You can check if something is in a role like so
        //    var isInRole = user.IsInRole("Student");

        //    return await _getCoursesCommand.ExecuteAsync();
        //}
    }
}
