using CodeEditorApi.Features.Courses.CreateCourses;
using CodeEditorApi.Features.Courses.DeleteCourses;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApi.Features.Courses.UpdateCourses;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]


    public class CoursesController : ControllerBase
    {
        private readonly IGetCoursesCommand _getCoursesCommand;
        private readonly IGetUserCreatedCoursesCommand _getUserCreatedCoursesCommand;
        private readonly ICreateCoursesCommand _createCoursesCommand;
        private readonly IUpdateCoursesCommand _updateCoursesCommand;
        private readonly IDeleteCoursesCommand _deleteCoursesCommand;

        public CoursesController(
            IGetCoursesCommand getCoursesCommand, 
            IGetUserCreatedCoursesCommand getUserCreatedCoursesCommand,
            ICreateCoursesCommand createCoursesCommand, 
            IUpdateCoursesCommand updateCoursesCommand, 
            IDeleteCoursesCommand deleteCoursesCommand)
        {
            _getCoursesCommand = getCoursesCommand;
            _getUserCreatedCoursesCommand = getUserCreatedCoursesCommand;
            _createCoursesCommand = createCoursesCommand;
            _updateCoursesCommand = updateCoursesCommand;
            _deleteCoursesCommand = deleteCoursesCommand;
        }

        /// <summary>
        /// Get's all courses for a single user
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [Authorize]
        public async Task<IEnumerable<Course>> GetUserCourses()
        {
            var userId = retrieveRequestUserId();
            return await _getCoursesCommand.ExecuteAsync(userId);
        }

        [HttpGet("Created")]
        [Authorize]
        public async Task<IEnumerable<Course>> GetUserCreatedCourses()
        {
            var userId = retrieveRequestUserId();
            return await _getUserCreatedCoursesCommand.ExecuteAsync(userId);
        }
        /// <summary>
        /// Creates a course for a user (admin/teacher role)
        /// </summary>
        /// <param name="createCourseBody">
        /// The course details for creating the new course
        /// </param>
        /// <returns></returns>
        [HttpPost("")]
        [Authorize]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] CreateCourseBody createCourseBody)
        {
            var userId = retrieveRequestUserId();
            return await _createCoursesCommand.ExecuteAsync(userId, createCourseBody);
        }

        /// <summary>
        /// Updates a course for a user (admin/teacher role)
        /// </summary>
        /// <param name="updateCourseBody">updated Course details for the existing course</param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPut("{courseId:int}")]
        [Authorize]
        public async Task<ActionResult<Course>> UpdateCourse(int courseId, [FromBody] UpdateCourseBody updateCourseBody)
        {
            var userId = retrieveRequestUserId();
            return await _updateCoursesCommand.ExecuteAsync(courseId, userId, updateCourseBody);
        }

        /// <summary>
        /// Delete a course
        /// </summary>
        /// <param name="courseId">
        /// Course for deletion
        /// </param>
        /// <returns></returns>
        [HttpDelete("{courseId:int}")]
        [Authorize]
        public async Task<ActionResult<Course>> DeleteCourse(int courseId)
        {
            var userId = retrieveRequestUserId();
            return await _deleteCoursesCommand.ExecuteAsync(userId, courseId);
        }

        private int retrieveRequestUserId()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
            try
            {
                return int.Parse(userId);
            }
            catch
            {
                throw new System.Exception($"User ID {userId} is invalid");
                //TODO: catch internal error of invalid userId...this should turn into a validation on it's own though. Then call validation in this method.
            }
        }
    }
}
