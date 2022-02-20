using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class GetCourseDetailsCommandTest : UnitTest<GetCourseDetailsCommand>
    {
        [Fact]
        public async Task ShouldReturnCourseDetails()
        {
            var course = fixture.Create<Course>();
            var user = fixture.Create<User>();
            var utList = fixture.Create<List<UserTutorial>>();
            var tutorials = fixture.Create<List<Tutorial>>();

            Freeze<IGetCourses>().Setup(gc => gc.GetCourseDetails(course.Id)).ReturnsAsync(course);
            Freeze<IGetTutorials>().Setup(gt => gt.GetCourseTutorials(course.Id)).ReturnsAsync(tutorials);
            Freeze<IGetTutorials>().Setup(gt => gt.GetUserRegisteredTutorials(course.Id, user.Id)).ReturnsAsync(utList);

            var actionResult = await Target().ExecuteAsync(user.Id, course.Id);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().NotBeNull();
            actionResult.Value.Should().BeOfType<GetCourseDetailsResponseBody>();
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfCourseNotFound()
        {
            var courseId = fixture.Create<int>();
            var user = fixture.Create<User>();

            var expected = ApiError.BadRequest($"Could not find course with ID {courseId}");

            Freeze<IGetCourses>().Setup(gc => gc.GetCourseDetails(courseId)).ReturnsAsync((Course)null);

            var actionResult = await Target().ExecuteAsync(user.Id, courseId);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected as BadRequestObjectResult);
        }
    }
}
