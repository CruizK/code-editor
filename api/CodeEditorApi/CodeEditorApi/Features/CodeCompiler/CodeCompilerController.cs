using CodeEditorApi.Features.CodeCompiler.Compile;
using CodeEditorApi.Features.Courses.CreateCourses;
using CodeEditorApi.Features.Courses.DeleteCourses;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApi.Features.Courses.RegisterUser;
using CodeEditorApi.Features.Courses.UnregisterUser;
using CodeEditorApi.Features.Courses.UpdateCourses;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CodeCompilerController : ControllerBase
    {
        private readonly ICompileCommand _compileCommand;

        public CodeCompilerController(ICompileCommand compileCommand)
        {
            _compileCommand = compileCommand;
        }

        /// <summary>
        /// Compiles code and returns output
        /// Adding more to comment for workflow tests
        /// </summary>
        /// <returns></returns>
        [HttpGet("Compile")]
        [Authorize]
        public async Task<ActionResult<string>> Compile([FromBody] CompileBody compileBody)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _compileCommand.ExecuteAsync(compileBody);
        }

    }
}
