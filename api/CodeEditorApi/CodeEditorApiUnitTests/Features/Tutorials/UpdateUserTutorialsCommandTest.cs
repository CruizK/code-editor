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
using CodeEditorApiDataAccess.StaticData;

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
                .With(ut => ut.Status, (int)TutorialStatus.NotStarted)
                .Create();

            var body = fixture.Build<UpdateUserTutorialBody>()
                .With(b => b.TutorialStatus, (int)TutorialStatus.InProgress)
                .Create();

            var updateUserTutorial = fixture.Build<UserTutorial>()
                .With(uut => uut.TutorialId, userTutorial.TutorialId)
                .With(uut => uut.UserId, user.Id)
                .With(uut => uut.Status, (int)TutorialStatus.InProgress)
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
                .With(ut => ut.Status, (int)TutorialStatus.NotStarted)
                .Create();

            var body = fixture.Build<UpdateUserTutorialBody>()
                .With(b => b.TutorialStatus, (int)TutorialStatus.Completed)
                .Create();

            var updateUserTutorial = fixture.Build<UserTutorial>()
                .With(uut => uut.TutorialId, userTutorial.TutorialId)
                .With(uut => uut.UserId, user.Id)
                .With(uut => uut.Status, (int)TutorialStatus.Completed)
                .Create();

            Freeze<IUpdateTutorials>().Setup(iut => iut.UpdateUserTutorial(userTutorial.TutorialId, user.Id, body)).ReturnsAsync(updateUserTutorial);

            var actionResult = await Target().ExecuteAsync(userTutorial.TutorialId, user.Id, body);

            actionResult.Value.Should().BeEquivalentTo(updateUserTutorial);
        }        
    }
}
