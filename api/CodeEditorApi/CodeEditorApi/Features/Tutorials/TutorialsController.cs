using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApi.Features.Tutorials.DeleteTutorials;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApi.Features.Tutorials.UnregisterUser;
using CodeEditorApi.Features.Tutorials.UpdateTutorials;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        private readonly IUpdateUserTutorialsCommand _updateUserTutorialsCommand;
        private readonly IUnregisterUserCommand _unregisterUserCommand;
        private readonly IGetUserTutorialsPerCourseCommand _getUserTutorialsPerCourseCommand;
        private readonly IGetUserTutorialsDetailsCommand _getUserTutorialsDetailsCommand;

        public TutorialsController(IGetTutorialsCommand getTutorialsCommand, ICreateTutorialsCommand createTutorialsCommand,
            IDeleteTutorialsCommand deleteTutorialsCommand, IUpdateTutorialsCommand updateTutorialsCommand,
            IGetUserCreatedTutorialsCommand getUserCreatedTutorialsCommand, IGetCourseTutorialsCommand getCourseTutorialsCommand,
            IGetUserLastInProgressTutorialCommand getUserLastInProgressTutorialCommand, IUpdateUserTutorialsCommand updateUserTutorialsCommand,
            IUnregisterUserCommand unregisterUserCommand, IGetUserTutorialsPerCourseCommand getUserTutorialsPerCourseCommand,
            IGetUserTutorialsDetailsCommand getUserTutorialsDetailsCommand)
        {
            _getTutorialsCommand = getTutorialsCommand;
            _createTutorialsCommand = createTutorialsCommand;
            _deleteTutorialsCommand = deleteTutorialsCommand;
            _updateTutorialsCommand = updateTutorialsCommand;
            _getUserCreatedTutorialsCommand = getUserCreatedTutorialsCommand;
            _getCourseTutorialsCommand = getCourseTutorialsCommand;
            _getUserLastInProgressTutorialCommand = getUserLastInProgressTutorialCommand;
            _updateUserTutorialsCommand = updateUserTutorialsCommand;
            _unregisterUserCommand = unregisterUserCommand;
            _getUserTutorialsPerCourseCommand = getUserTutorialsPerCourseCommand;
            _getUserTutorialsDetailsCommand = getUserTutorialsDetailsCommand;
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
        /// Get A User's Tutorial statuses for a specific Course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("GetUserTutorialsOnCourse/{courseId:int}")]
        [Authorize]
        public async Task<ActionResult<List<UserTutorial>>> GetUserTutorialsPerCourse(int courseId)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _getUserTutorialsPerCourseCommand.ExecuteAsync(courseId, userId);
        }

        /// <summary>
        /// Get User's progress on each Tutorial for a specific Course AND the details of that Tutorial
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("GetUserTutorialsDetails/{courseId:int}")]
        [Authorize]
        public async Task<ActionResult<List<UserTutorialDetailsBody>>> GetUserTutorialsDetails(int courseId)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _getUserTutorialsDetailsCommand.ExecuteAsync(courseId, userId);
        }

        /// <summary>
        /// Create a Tutorial
        /// </summary>
        /// <param name="createTutorialsBody"></param>
        /// <returns></returns>
        [HttpPost("CreateTutorial")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> CreateTutorial([FromBody] CreateTutorialsBody createTutorialsBody)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _createTutorialsCommand.ExecuteAsync(userId, createTutorialsBody);
        }

        /// <summary>
        /// Delete A Tutorial based on its ID
        /// </summary>
        /// <param name="tutorialId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTutorial/{tutorialId:int}")]
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
        [HttpPut("UpdateTutorial/{tutorialId:int}")]
        [Authorize]
        public async Task<ActionResult<Tutorial>> UpdateTutorials(int tutorialId, [FromBody] CreateTutorialsBody createTutorialsBody)
        {
            return await _updateTutorialsCommand.ExecuteAsync(tutorialId, createTutorialsBody);
        }

        /// <summary>
        /// Update the status of a Tutorial that a specific User is registered to
        /// </summary>
        /// <param name="tutorialId"></param>
        /// <param name="updateUserTutorialBody"></param>
        /// <returns></returns>
        [HttpPut("UpdateUserTutorial/{tutorialId:int}")]
        [Authorize]
        public async Task<ActionResult<UserTutorial>> UpdateUserTutorials(int tutorialId, [FromBody] UpdateUserTutorialBody updateUserTutorialBody)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _updateUserTutorialsCommand.ExecuteAsync(tutorialId, userId, updateUserTutorialBody);
        }

        /// <summary>
        /// Unregister a User from all Tutorials under a Course - use for when a User unregisters from a Course
        /// </summary>
        /// <param name="unregisterUserTutorialBody"></param>
        /// <returns></returns>
        [HttpDelete("UnregisterUser")]
        [Authorize]
        public async Task<ActionResult<List<UserTutorial>>> UnregisterUserTutorials([FromBody] UnregisterUserTutorialBody unregisterUserTutorialBody)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _unregisterUserCommand.ExecuteAsync(unregisterUserTutorialBody.courseId, userId);
        }
    }
}
