using CodeEditorApi.Errors;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface IGetCourseDetailsCommand
    {
        public Task<ActionResult<GetCourseDetailsResponseBody>> ExecuteAsync(int userId, int courseId);
    }
    public class GetCourseDetailsCommand : IGetCourseDetailsCommand
    {
        private readonly IGetCourses _getCourses;
        private readonly IGetTutorials _getTutorials;

        public GetCourseDetailsCommand(IGetCourses getCourses, IGetTutorials getTutorials)
        {
            _getCourses = getCourses;
            _getTutorials = getTutorials;
        }
        public async Task<ActionResult<GetCourseDetailsResponseBody>> ExecuteAsync(int userId, int courseId)
        {
            var course = await _getCourses.GetCourseDetails(courseId);
            
            if(course == null)
            {
                return ApiError.BadRequest($"Could not find course with ID {courseId}");
            }

            var tutorials = await _getTutorials.GetCourseTutorials(courseId);
            var studentTutorialProgress = await _getTutorials.GetUserRegisteredTutorials(courseId, userId);

            var body = new GetCourseDetailsResponseBody
            {
                CourseDetails = course,
                CourseTutorials = tutorials,
                userTutorialList = studentTutorialProgress
            };

            return body;
        }
    }
}
