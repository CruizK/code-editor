using CodeEditorApi.Features.Tutorials.UpdateTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using AutoFixture;
using CodeEditorApi.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class UpdateUserTutorialsCommandTest : UnitTest<UpdateUserTutorialsCommand>
    {
        [Fact]
        public async Task ShouldReturnInProgressUserTutorial()
        {
            var user = fixture.Create<User>();
            var userTutorial = fixture.Build<UserTutorial>()
                .With(ut => ut.UserId, user.Id)
                .With(ut => ut.InProgress, false)
                .With(ut => ut.IsCompleted, false)
                .Create();

            var body = fixture.Build<UpdateUserTutorialBody>()
                .With(b => b.InProgress, true)
                .With(b => b.IsCompleted, false)
                .Create();

            var updateUserTutorial = fixture.Build<UserTutorial>()
                .With(uut => uut.TutorialId, userTutorial.TutorialId)
                .With(uut => uut.UserId, user.Id)
                .With(uut => uut.InProgress, true)
                .With(uut => uut.IsCompleted, false)
                .Create();

            Freeze<IUpdateTutorials>().Setup(iut => iut.UpdateUserTutorial(userTutorial.TutorialId, user.Id, body)).ReturnsAsync(updateUserTutorial);

            var actionResult = await Target().ExecuteAsync(userTutorial.TutorialId, user.Id, body);

            actionResult.Value.Should().BeEquivalentTo(updateUserTutorial);
        }

        [Fact]
        public async Task ShouldReturnIsCompletedUserTutorial()
        {
            var user = fixture.Create<User>();
            var userTutorial = fixture.Build<UserTutorial>()
                .With(ut => ut.UserId, user.Id)
                .With(ut => ut.InProgress, false)
                .With(ut => ut.IsCompleted, false)
                .Create();

            var body = fixture.Build<UpdateUserTutorialBody>()
                .With(b => b.InProgress, false)
                .With(b => b.IsCompleted, true)
                .Create();

            var updateUserTutorial = fixture.Build<UserTutorial>()
                .With(uut => uut.TutorialId, userTutorial.TutorialId)
                .With(uut => uut.UserId, user.Id)
                .With(uut => uut.InProgress, false)
                .With(uut => uut.IsCompleted, true)
                .Create();

            Freeze<IUpdateTutorials>().Setup(iut => iut.UpdateUserTutorial(userTutorial.TutorialId, user.Id, body)).ReturnsAsync(updateUserTutorial);

            var actionResult = await Target().ExecuteAsync(userTutorial.TutorialId, user.Id, body);

            actionResult.Value.Should().BeEquivalentTo(updateUserTutorial);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfInProgressAndCompletedIsTrue()
        {
            var user = fixture.Create<User>();
            var userTutorial = fixture.Build<UserTutorial>()
                .With(ut => ut.UserId, user.Id)
                .With(ut => ut.InProgress, false)
                .With(ut => ut.IsCompleted, false)
                .Create();

            var body = fixture.Build<UpdateUserTutorialBody>()
                .With(b => b.InProgress, true)
                .With(b => b.IsCompleted, true)
                .Create();

            var expected = new BadRequestError($"Cannot submit a UserTutorial that is both in progress and completed.");

            Freeze<IUpdateTutorials>().Setup(iut => iut.UpdateUserTutorial(userTutorial.TutorialId, user.Id, body)).ReturnsAsync((UserTutorial)null);

            var actionResult = await Target().ExecuteAsync(userTutorial.TutorialId, user.Id, body);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }
    }
}
