using CodeEditorApi.Features.Courses.UpdateCourses;
using CodeEditorApiUnitTests.Helpers;
using AutoFixture;
using Xunit;
using System.Threading.Tasks;
using CodeEditorApiDataAccess.Data;
using Moq;
using FluentAssertions;
using System.Linq;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApi.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class UpdateCoursesCommandTest : UnitTest<UpdateCoursesCommand>
    {
        [Fact]
        public async Task ShouldReturnBadRequestIfUserDidNotCreateCourse()
        {
            var user = fixture.Create<User>();
            var course = fixture.Create<Course>();
            var body = fixture.Create<UpdateCourseBody>();
            var expected = new BadRequestError("Only the author of a course may edit it");

            Freeze<IUpdateCourses>().Setup(uc => uc.ExecuteAsync(course.Id, body)).ReturnsAsync((Course)null);

            var actionResult = await Target().ExecuteAsync(course.Id, user.Id, body);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task ShouldReturnUpdatedCourse()
        {
            var user = fixture.Create<User>();            
            var userCreatedCourses = fixture.Build<Course>()
                .With(c => c.Author, user.Id)
                .CreateMany().ToList();
            var course = userCreatedCourses.First();
            var body = fixture.Create<UpdateCourseBody>();

            Freeze<IGetCourses>().Setup(gc => gc.GetUserCreatedCourses(user.Id)).ReturnsAsync(userCreatedCourses);
            Freeze<IUpdateCourses>().Setup(uc => uc.ExecuteAsync(course.Id, body)).ReturnsAsync(course);

            var actionResult = await Target().ExecuteAsync(course.Id, user.Id, body);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().Be(course);
        }
    }
}
