using AutoFixture;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class GetUserTutorialsPerCourseCommandTest : UnitTest<GetUserTutorialsPerCourseCommand>
    {
        [Fact]
        public async Task ShouldReturnUserTutorials()
        {
            var courseId = It.IsAny<int>();
            var userId = It.IsAny<int>();
            var userTutorials = fixture.Build<UserTutorial>()
                .With(ut => ut.UserId, userId)
                .CreateMany()
                .ToList();

            Freeze<IGetTutorials>().Setup(gt => gt.GetUserRegisteredTutorials(courseId, userId)).ReturnsAsync(userTutorials);

            var actionResult = await Target().ExecuteAsync(courseId, userId);

            actionResult.Value.Should().NotBeNull();
            actionResult.Value.Should().BeEquivalentTo(userTutorials);
        }
    }
}
