using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApi.Features.Courses.RegisterUser;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class RegisterUserCommandTest : UnitTest<RegisterUserCommand>
    {
        [Fact]
        public async Task ShouldReturnRegisteredCourse()
        {
            var userRegisteredCourse = fixture.Create<UserRegisteredCourse>();
            var body = fixture.Create<RegisterUserBody>();
            var courses = fixture.CreateMany<Course>();
            var userId = fixture.Create<int>();

            Freeze<IGetCourses>().Setup(x => x.GetUserCourses(userId)).ReturnsAsync(courses.ToList());
            Freeze<IRegisterUser>().Setup(x => x.ExecuteAsync(userId, It.IsAny<UserRegisteredCourse>())).ReturnsAsync(userRegisteredCourse);

            var actionResult = await Target().ExecuteAsync(userId, body);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().BeEquivalentTo(userRegisteredCourse);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfAlreadyRegistered()
        {
            var userRegisteredCourse = fixture.Create<UserRegisteredCourse>();
            var body = fixture.Create<RegisterUserBody>();
            var courses = fixture.Build<Course>()
                .With(x => x.Id, body.CourseId)
                .CreateMany();
            var userId = fixture.Create<int>();

            var expected = new BadRequestError("User already registered to course");

            Freeze<IGetCourses>().Setup(x => x.GetUserCourses(userId)).ReturnsAsync(courses.ToList());
            Freeze<IRegisterUser>().Setup(x => x.ExecuteAsync(userId, userRegisteredCourse)).ReturnsAsync(userRegisteredCourse);

            var actionResult = await Target().ExecuteAsync(userId, body);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);

        }
    }
}
