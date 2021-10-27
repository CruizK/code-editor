using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialsController : ControllerBase
    {
        private readonly IGetTutorialsCommand _getTutorialsCommand;
        private readonly ICreateTutorialsCommand _createTutorialsCommand;
        public TutorialsController(IGetTutorialsCommand getTutorialsCommand, ICreateTutorialsCommand createTutorialsCommand)
        {
            _getTutorialsCommand = getTutorialsCommand;
            _createTutorialsCommand = createTutorialsCommand;
        }

        /// <summary>
        /// Gets a tutorial created by a User by Tutorial Id
        /// </summary>
        [HttpGet("GetUserTutorials/{TutorialId:int}")]
        [Authorize]
        public async Task<Tutorial> GetUserTutorials(int TutorialId)
        {
            var tutorial = new GetTutorialsBody{
                TutorialId = TutorialId
            };

            return await _getTutorialsCommand.ExecuteAsync(tutorial);
        }

        [HttpPost("CreateTutorials")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> CreateTutorial([FromBody] CreateTutorialsBody createTutorialsBody)
        {
            return await _createTutorialsCommand.ExecuteAsync(createTutorialsBody);
        }
    }
}
