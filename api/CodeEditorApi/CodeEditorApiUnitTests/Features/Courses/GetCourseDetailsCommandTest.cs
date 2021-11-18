using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
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

            Freeze<IGetCourses>().Setup(gc => gc.GetCourseDetails(course.Id)).ReturnsAsync(course);

            var actionResult = await Target().ExecuteAsync(course.Id);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().BeEquivalentTo(course);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfCourseNotFound()
        {
            var courseId = fixture.Create<int>();
            var expected = ApiError.BadRequest($"Could not find course with ID {courseId}");
            Freeze<IGetCourses>().Setup(gc => gc.GetCourseDetails(courseId)).ReturnsAsync((Course)null);

            var actionResult = await Target().ExecuteAsync(courseId);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected as BadRequestObjectResult);
        }
    }
}
