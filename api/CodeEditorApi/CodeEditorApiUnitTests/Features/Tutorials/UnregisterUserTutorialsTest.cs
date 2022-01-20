using AutoFixture;
using CodeEditorApi.Features.Tutorials.UnregisterUser;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class UnregisterUserTutorialsTest : UnitTest<UnregisterUserCommand>
    {
        [Fact]
        public async Task ShouldReturnUserTutorials()
        {
            var userId = It.IsAny<int>();

            var courseId = It.IsAny<int>();

            var tutorials = fixture.Build<Tutorial>()
                .With(t => t.CourseId, courseId)
                .CreateMany().ToList();

            var userTutorials = It.IsAny<List<UserTutorial>>();

            Freeze<IUnregisterUser>().Setup(uru => uru.ExecuteAsync(courseId, userId)).ReturnsAsync(userTutorials);

            var actionResult = await Target().ExecuteAsync(courseId, userId);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().BeEquivalentTo(userTutorials);

        }
    }
}
