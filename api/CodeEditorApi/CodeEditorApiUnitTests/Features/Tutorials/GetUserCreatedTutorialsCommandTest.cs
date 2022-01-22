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
    public class GetUserCreatedTutorialsCommandTest : UnitTest<GetUserCreatedTutorialsCommand>
    {
        [Fact]
        public async Task ShouldReturnTutorialsWithGivenUserId()
        {
            var user = fixture.Create<User>();
            var tutorials = fixture.Build<Tutorial>()
                .With(t => t.Author, user.Id).CreateMany().ToList();

            Freeze<IGetTutorials>().Setup(g => g.GetUserCreatedTutorialsList(user.Id)).ReturnsAsync(tutorials);

            var actionResult = await Target().ExecuteAsync(user.Id);

            actionResult.Value.Should().BeEquivalentTo(tutorials);
        }
    }
}
