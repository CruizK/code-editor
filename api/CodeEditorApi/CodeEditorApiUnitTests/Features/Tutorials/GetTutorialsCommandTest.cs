using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class GetTutorialsCommandTest : UnitTest<GetTutorialsCommand>
    {
        [Fact]
        public async Task ShouldReturnBadRequestIfTutorialDoesNotExist()
        {
            int tutorialId = int.MaxValue;
            var expected = new BadRequestError($"Cannot retrieve Tutorial with id {tutorialId}");

            Freeze<IGetTutorials>()
                .Setup(g => g.GetUserTutorials(tutorialId))
                .ReturnsAsync((Tutorial)null);

            var actionResult = await Target().ExecuteAsync(tutorialId);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task ShouldReturnTutorial()
        {
            var tutorial = fixture.Create<Tutorial>();

            Freeze<IGetTutorials>().Setup(g => g.GetUserTutorials(tutorial.Id)).ReturnsAsync(tutorial);

            var actionResult = await Target().ExecuteAsync(tutorial.Id);

            actionResult.Value.Should().Be(tutorial);
        }
    }
}
