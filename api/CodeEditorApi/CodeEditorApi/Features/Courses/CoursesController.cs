using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using CodeEditorApi.Features.Courses.CreateCourses;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApi.Features.Courses.UpdateCourses;
using CodeEditorApi.Features.Courses.DeleteCourses;

namespace CodeEditorApi.Features.Courses
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]

    /// <summary>
    /// Controls the direction of which CRUD operation/API request is called
    /// </summary>
    public class CoursesController : ControllerBase
    {
        private readonly IGetCoursesCommand _getCoursesCommand;
        private readonly ICreateCoursesCommand _createCoursesCommand;
        private readonly IUpdateCoursesCommand _updateCoursesCommand;
        private readonly IDeleteCoursesCommand _deleteCoursesCommand;

        public CoursesController(IGetCoursesCommand getCoursesCommand, ICreateCoursesCommand createCoursesCommand, IUpdateCoursesCommand updateCoursesCommand, IDeleteCoursesCommand deleteCoursesCommand)
        {
            _getCoursesCommand = getCoursesCommand;
            _createCoursesCommand = createCoursesCommand;
            _updateCoursesCommand = updateCoursesCommand;
            _deleteCoursesCommand = deleteCoursesCommand;
        }

        /// <summary>
        /// Get's all courses for a single user
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCourse")]
        [Authorize]
        public async Task<IEnumerable<Course>> GetCourses()
        {
            var userId = retrieveRequestUserId();
            return await _getCoursesCommand.ExecuteAsync(userId);
        }

        /// <summary>
        /// Creates a course for a user (admin/teacher role)
        /// </summary>
        /// <param name="course">
        /// The course details for creating the new course
        /// </param>
        /// <returns></returns>
        [HttpPost("CreateCourse")]
        [Authorize]
        public async Task CreateCourse([FromBody] Course course)
        {
            var userId = retrieveRequestUserId();
            await _createCoursesCommand.ExecuteAsync(userId, course);
        }

        /// <summary>
        /// Updates a course for a user (admin/teacher role)
        /// </summary>
        /// <param name="course">
        /// updated Course details for the existing course
        /// </param>
        /// <returns></returns>
        [HttpPut("UpdateCourse")]
        [Authorize]
        public async Task UpdateCourse([FromBody] Course course)
        {
            await _updateCoursesCommand.ExecuteAsync(course);
        }

        /// <summary>
        /// Delete a course
        /// </summary>
        /// <param name="course">
        /// Course for deletion
        /// </param>
        /// <returns></returns>
        [HttpDelete("DeleteCourses")]
        [Authorize]
        public async Task DeleteCourse([FromBody] Course course)
        {
            await _deleteCoursesCommand.ExecuteAsync(course);
        }
        private int retrieveRequestUserId()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
            try
            {
                return int.Parse(userId);
            }
            catch (System.FormatException e)
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
