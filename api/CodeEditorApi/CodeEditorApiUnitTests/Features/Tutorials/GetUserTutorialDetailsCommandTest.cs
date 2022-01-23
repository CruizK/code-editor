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
    public class GetUserTutorialDetailsCommandTest : UnitTest<GetUserTutorialsDetailsCommand>
    {
        [Fact]
        public async Task ShouldReturnUserTutorialDetailsBodyList()
        {
            var userId = It.IsAny<int>();
            var courseId = It.IsAny<int>();

            var userTutorialDetails = fixture.Build<UserTutorialDetailsBody>()
                .With(utdb => utdb.UserId, userId)
                .CreateMany().ToList();

            Freeze<IGetTutorials>().Setup(gt => gt.GetUserRegisteredTutorialsWithCourseTutorialDetails(courseId, userId)).ReturnsAsync(userTutorialDetails);

            var actionResult = await Target().ExecuteAsync(courseId, userId);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().BeEquivalentTo(userTutorialDetails);

        }
    }
}
