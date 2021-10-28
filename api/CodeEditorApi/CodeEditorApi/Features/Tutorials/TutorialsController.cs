using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApi.Features.Tutorials.DeleteTutorials;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApi.Features.Tutorials.UpdateTutorials;
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
        private readonly IDeleteTutorialsCommand _deleteTutorialsCommand;
        private readonly IUpdateTutorialsCommand _updateTutorialsCommand;
        private readonly IGetUserCreatedTutorialsCommand _getUserCreatedTutorialsCommand;
        private readonly IGetCourseTutorialsCommand _getCourseTutorialsCommand;
        public TutorialsController(IGetTutorialsCommand getTutorialsCommand, ICreateTutorialsCommand createTutorialsCommand,
            IDeleteTutorialsCommand deleteTutorialsCommand, IUpdateTutorialsCommand updateTutorialsCommand,
            IGetUserCreatedTutorialsCommand getUserCreatedTutorialsCommand, IGetCourseTutorialsCommand getCourseTutorialsCommand)
        {
            _getTutorialsCommand = getTutorialsCommand;
            _createTutorialsCommand = createTutorialsCommand;
            _deleteTutorialsCommand = deleteTutorialsCommand;
            _updateTutorialsCommand = updateTutorialsCommand;
            _getUserCreatedTutorialsCommand = getUserCreatedTutorialsCommand;
            _getCourseTutorialsCommand = getCourseTutorialsCommand;
        }

        /// <summary>
        /// Gets a tutorial created by a User by Tutorial Id
        /// </summary>
        [HttpGet("GetUserTutorials/{TutorialId:int}")]
        [Authorize]
        public async Task<Tutorial> GetUserTutorials(int tutorialId)
        {
            //TODO: don't do the below thing...fix the GetTutorialBody connection
            var tutorial = new GetTutorialsBody {
                TutorialId = tutorialId
            };

            return await _getTutorialsCommand.ExecuteAsync(tutorial);
        }

        [HttpGet("GetUserCreatedTutorials/{UserId:int}")]
        [Authorize]
        public async Task<ActionResult<List<Tutorial>>> GetUserCreatedTutorials(int userId)
        {
            return await _getUserCreatedTutorialsCommand.ExecuteAsync(userId);
        }

        [HttpGet("GetCourseTutorials/{CourseId:int}")]
        [Authorize]
        public async Task<ActionResult<List<Tutorial>>> GetCourseTutorials(int courseId)
        {
            return await _getCourseTutorialsCommand.ExecuteAsync(courseId);
        }

        [HttpPost("CreateTutorials")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> CreateTutorial([FromBody] CreateTutorialsBody createTutorialsBody)
        {
            return await _createTutorialsCommand.ExecuteAsync(createTutorialsBody);
        }

        [HttpDelete("DeleteTutorials/{TutorialId:int}")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> DeleteTutorials(int tutorialId)
        {
            return await _deleteTutorialsCommand.ExecuteAsync(tutorialId);
        }

        [HttpPut("UpdateTutorials/{TutorialId:int}")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> UpdateTutorials(int tutorialId, [FromBody] CreateTutorialsBody createTutorialsBody)
        {
            return await _updateTutorialsCommand.ExecuteAsync(tutorialId, createTutorialsBody);
        }
    }
}
