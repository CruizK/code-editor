using CodeEditorApi.Features.Courses.GetCourses;
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

        public CoursesController(IGetCoursesCommand getCoursesCommand)
        {
            _getCoursesCommand = getCoursesCommand;
        }

        /// <summary>
        /// Get's all courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _getCoursesCommand.ExecuteAsync();
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
