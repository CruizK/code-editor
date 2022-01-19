﻿using CodeEditorApi.Features.Courses.CreateCourses;
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
    public class CoursesController : ControllerBase
    {
        private readonly IGetCoursesCommand _getCoursesCommand;
        private readonly IGetUserCreatedCoursesCommand _getUserCreatedCoursesCommand;
        private readonly ICreateCoursesCommand _createCoursesCommand;
        private readonly IUpdateCoursesCommand _updateCoursesCommand;
        private readonly IDeleteCoursesCommand _deleteCoursesCommand;
        private readonly IRegisterUserCommand _registerUserCommand;
        private readonly IUnregisterUserCommand _unregisterUserCommand;
        private readonly IGetCourseDetailsCommand _getCourseDetailsCommand;
        private readonly IGetAllPublishedCoursesCommand _getAllPublishedCoursesCommand;
        private readonly IGetMostPopularCoursesCommand _getMostPopularCoursesCommand;

        public CoursesController(
            IGetCoursesCommand getCoursesCommand, 
            IGetUserCreatedCoursesCommand getUserCreatedCoursesCommand,
            ICreateCoursesCommand createCoursesCommand, 
            IUpdateCoursesCommand updateCoursesCommand, 
            IDeleteCoursesCommand deleteCoursesCommand,
            IRegisterUserCommand registerUserCommand,
            IUnregisterUserCommand unregisterUserCommand,
            IGetCourseDetailsCommand getCourseDetailsCommand,
            IGetAllPublishedCoursesCommand getAllPublishedCoursesCommand,
            IGetMostPopularCoursesCommand getMostPopularCoursesCommand)
        {
            _getCoursesCommand = getCoursesCommand;
            _getUserCreatedCoursesCommand = getUserCreatedCoursesCommand;
            _createCoursesCommand = createCoursesCommand;
            _updateCoursesCommand = updateCoursesCommand;
            _deleteCoursesCommand = deleteCoursesCommand;
            _registerUserCommand = registerUserCommand;
            _unregisterUserCommand = unregisterUserCommand;
            _getCourseDetailsCommand = getCourseDetailsCommand;
            _getAllPublishedCoursesCommand = getAllPublishedCoursesCommand;
            _getMostPopularCoursesCommand = getMostPopularCoursesCommand;
        }

        /// <summary>
        /// Gets all courses for a single user
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserCourses")]
        [Authorize]
        public async Task<ActionResult<List<Course>>> GetUserCourses()
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _getCoursesCommand.ExecuteAsync(userId);
        }

        /// <summary>
        /// Gets all courses created by a user
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserCreatedCourses")]
        [Authorize]
        public async Task<ActionResult<List<Course>>> GetUserCreatedCourses()
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _getUserCreatedCoursesCommand.ExecuteAsync(userId);
        }

        /// <summary>
        /// Gets all the details for a single course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet("GetCourseDetails/{courseId:int}")]
        [Authorize]
        public async Task<ActionResult<Course>> GetCourseDetails(int courseId)
        {
            return await _getCourseDetailsCommand.ExecuteAsync(courseId);
        }

        /// <summary>
        /// Gets all courses that are published
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllPublishedCourses")]
        [Authorize]
        public async Task<ActionResult<List<Course>>> GetAllPublishedCourses()
        {
            return await _getAllPublishedCoursesCommand.GetAllPublishedCourses();
        }

        /// <summary>
        /// Gets all courses that are published according to the latest updated courses
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllPublishedCoursesSortByModifyDate")]
        [Authorize]
        public async Task<ActionResult<List<Course>>> GetAllPublishedCoursesSortByModifyDate()
        {
            return await _getAllPublishedCoursesCommand.GetAllPublishedCoursesSortByModifyDate();
        }

        /// <summary>
        /// Gets the top most popular courses
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMostPopularCourses")]
        [Authorize]
        public async Task<ActionResult<List<Course>>> GetMostPopularCourses()
        {
            return await _getMostPopularCoursesCommand.ExecuteAsync();
        }

        /// <summary>
        /// Creates a course for a user (admin/teacher role)
        /// </summary>
        /// <param name="createCourseBody">
        /// The course details for creating the new course
        /// </param>
        /// <returns></returns>
        [HttpPost("CreateCourse")]
        [Authorize]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] CreateCourseBody createCourseBody)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _createCoursesCommand.ExecuteAsync(userId, createCourseBody);
        }

        /// <summary>
        /// Creates a course for a user (admin/teacher role)
        /// </summary>
        /// <param name="registerUserBody">
        /// The course details for registering a user
        /// </param>
        /// <returns></returns>
        [HttpPost("RegisterUser")]
        [Authorize]
        public async Task<ActionResult<UserRegisteredCourse>> RegisterUser([FromBody] RegisterUserBody registerUserBody)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _registerUserCommand.ExecuteAsync(userId, registerUserBody);
        }

        /// <summary>
        /// Creates a course for a user (admin/teacher role)
        /// </summary>
        /// <param name="unregisterUserBody">
        /// The course details for unregistering a user
        /// </param>
        /// <returns></returns>
        [HttpPost("UnregisterUser")]
        [Authorize]
        public async Task<ActionResult<UserRegisteredCourse>> UnregisterUser([FromBody] UnregisterUserBody unregisterUserBody)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _unregisterUserCommand.ExecuteAsync(userId, unregisterUserBody);
        }

        /// <summary>
        /// Updates a course for a user (admin/teacher role)
        /// </summary>
        /// <param name="updateCourseBody">updated Course details for the existing course</param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPut("UpdateCourse/{courseId:int}")]
        [Authorize]
        public async Task<ActionResult<Course>> UpdateCourse(int courseId, [FromBody] UpdateCourseBody updateCourseBody)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _updateCoursesCommand.ExecuteAsync(courseId, userId, updateCourseBody);
        }

        /// <summary>
        /// Delete a course
        /// </summary>
        /// <param name="courseId">
        /// Course for deletion
        /// </param>
        /// <returns></returns>
        [HttpDelete("DeleteCourse/{courseId:int}")]
        [Authorize]
        public async Task<ActionResult<Course>> DeleteCourse(int courseId)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            return await _deleteCoursesCommand.ExecuteAsync(userId, courseId);
        }

        
    }
}
