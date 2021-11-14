using CodeEditorApi.Features.Tutorials.RegisterUser;
using CodeEditorApiUnitTests.Helpers;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using CodeEditorApiDataAccess.Data;
using System.Linq;
using Moq;
using FluentAssertions;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class RegisterUserTutorialCommandTest : UnitTest<RegisterUserTutorialCommand>
    {
        [Fact]
        public async Task ShouldReturnTutorialList()
        {
            var userId = fixture.Create<int>();
            var courseId = fixture.Create<int>();
            var tutorials = fixture.Build<Tutorial>()
                .With(t => t.CourseId, courseId)
                .CreateMany()
                .ToList();

            Freeze<IRegisterUserTutorial>().Setup(rut => rut.ExecuteAsync(userId, tutorials)).ReturnsAsync(tutorials);

            var actionResult = await Target().ExecuteAsync(userId, tutorials);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().BeEquivalentTo(tutorials);

        }
    }
}
