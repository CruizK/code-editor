using CodeEditorApi.Errors;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetUserLastInProgressTutorialCommand
    {
        public Task<ActionResult<Tutorial>> ExecuteAsync(int userId, int courseId);
    }
    public class GetUserLastInProgressTutorialCommand : IGetUserLastInProgressTutorialCommand
    {
        private readonly IGetTutorials _getTutorials;
        private readonly IGetCourses _getCourses;

        public GetUserLastInProgressTutorialCommand(
            IGetTutorials getTutorials,
            IGetCourses getCourses)
        {
            _getTutorials = getTutorials;
            _getCourses = getCourses;
        }

        public async Task<ActionResult<Tutorial>> ExecuteAsync(int userId, int courseId)
        {
            var userRegisteredForCourse = await _getCourses.GetUserCourses(userId);
            if (userRegisteredForCourse.Count() == 0)
            {
                return ApiError.BadRequest($"User is not registered for course with id {courseId}");
            }

            //this needs to grab tutorials based on both userId and courseId (tutorials under course)
            var userRegisteredTutorials = await _getTutorials.GetUserRegisteredTutorials(userId);

            if (!userRegisteredTutorials.Where(t => t.InProgress == true).Any())
            {
                return ApiError.BadRequest($"User has not started any tutorial for course with id {courseId}");
            }

            var tutorial = await _getTutorials.GetUserLastInProgressTutorial(userId, courseId);

            if(tutorial == null)
            {
                return ApiError.BadRequest($"In Progress Tutorial could not be found.");
            }

            return tutorial;
        }
    }
}
