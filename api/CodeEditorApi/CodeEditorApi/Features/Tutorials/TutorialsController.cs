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
        private readonly IGetUserLastInProgressTutorialCommand _getUserLastInProgressTutorialCommand;
        public TutorialsController(IGetTutorialsCommand getTutorialsCommand, ICreateTutorialsCommand createTutorialsCommand,
            IDeleteTutorialsCommand deleteTutorialsCommand, IUpdateTutorialsCommand updateTutorialsCommand,
            IGetUserCreatedTutorialsCommand getUserCreatedTutorialsCommand, IGetCourseTutorialsCommand getCourseTutorialsCommand,
            IGetUserLastInProgressTutorialCommand getUserLastInProgressTutorialCommand)
        {
            _getTutorialsCommand = getTutorialsCommand;
            _createTutorialsCommand = createTutorialsCommand;
            _deleteTutorialsCommand = deleteTutorialsCommand;
            _updateTutorialsCommand = updateTutorialsCommand;
            _getUserCreatedTutorialsCommand = getUserCreatedTutorialsCommand;
            _getCourseTutorialsCommand = getCourseTutorialsCommand;
            _getUserLastInProgressTutorialCommand = getUserLastInProgressTutorialCommand;
        }

        /// <summary>
        /// Get a Tutorial's details based on the User who created it
        /// </summary>
        /// <param name="tutorialId"></param>
        /// <returns></returns>
        [HttpGet("UserTutorialDetails/{tutorialId:int}")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> GetUserTutorialDetails(int tutorialId)
        {
            return await _getTutorialsCommand.ExecuteAsync(tutorialId);
        }

        /// <summary>
        /// Get all tutorials that a User created
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("UserCreatedTutorials/{userId:int}")]
        [Authorize]
        public async Task<ActionResult<List<Tutorial>>> GetUserCreatedTutorials(int userId)
        {
            return await _getUserCreatedTutorialsCommand.ExecuteAsync(userId);
        }

        /// <summary>
        /// Get all tutorials under a Course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("CourseTutorials/{courseId:int}")]
        [Authorize]
        public async Task<ActionResult<List<Tutorial>>> GetCourseTutorials(int courseId)
        {
            return await _getCourseTutorialsCommand.ExecuteAsync(courseId);
        }

        /// <summary>
        /// get the Tutorial a User was last working on (in progress) for a Course view
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("GetUserLastInProgressTutorial/{courseId:int}")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> GetUserLastInProgressTutorial(int courseId)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _getUserLastInProgressTutorialCommand.ExecuteAsync(userId, courseId);
            
        }

        /// <summary>
        /// Create a Tutorial
        /// </summary>
        /// <param name="createTutorialsBody"></param>
        /// <returns></returns>
        [HttpPost("")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> CreateTutorial([FromBody] CreateTutorialsBody createTutorialsBody)
        {
            return await _createTutorialsCommand.ExecuteAsync(createTutorialsBody);
        }

        /// <summary>
        /// Delete A Tutorial based on its ID
        /// </summary>
        /// <param name="tutorialId"></param>
        /// <returns></returns>
        [HttpDelete("{tutorialId:int}")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> DeleteTutorials(int tutorialId)
        {
            return await _deleteTutorialsCommand.ExecuteAsync(tutorialId);
        }

        /// <summary>
        /// Update a Tutorial based on its ID
        /// </summary>
        /// <param name="tutorialId"></param>
        /// <param name="createTutorialsBody"></param>
        /// <returns></returns>
        [HttpPut("{tutorialId:int}")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> UpdateTutorials(int tutorialId, [FromBody] CreateTutorialsBody createTutorialsBody)
        {
            return await _updateTutorialsCommand.ExecuteAsync(tutorialId, createTutorialsBody);
        }
    }
}
