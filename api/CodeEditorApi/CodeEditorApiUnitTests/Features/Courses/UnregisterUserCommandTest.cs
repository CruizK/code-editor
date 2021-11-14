using AutoFixture;
using CodeEditorApi.Features.Courses.UnregisterUser;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class UnregisterUserCommandTest : UnitTest<UnregisterUserCommand>
    {
        [Fact]
        public async Task ShouldReturnUserRegisteredCourse()
        {
            var userRegisteredCourse = fixture.Create<UserRegisteredCourse>();
            var body = fixture.Create<UnregisterUserBody>();
            var courses = fixture.CreateMany<Course>();
            var userId = fixture.Create<int>();

            Freeze<IUnregisterUser>().Setup(x => x.ExecuteAsync(userId, body.CourseId)).ReturnsAsync(userRegisteredCourse);

            var actionResult = await Target().ExecuteAsync(userId, body);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().BeEquivalentTo(userRegisteredCourse);
        }
    }
}
