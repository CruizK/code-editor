using AutoFixture;
using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApi.Features.Tutorials.UpdateTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class UpdateTutorialsCommandTest : UnitTest<UpdateTutorialsCommand>
    {
        [Fact]
        public async Task ShouldReturnTutorial()
        {
            var user = fixture.Create<User>();
            var tutorial = fixture.Build<Tutorial>()
                .With(t => t.Author, user.Id)
                .Create();
            var body = fixture.Create<CreateTutorialsBody>();

            Freeze<IUpdateTutorials>().Setup(u => u.UpdateTutorial(tutorial.Id, body)).ReturnsAsync(tutorial);

            var actionResult = await Target().ExecuteAsync(tutorial.Id, body);

            actionResult.Value.Should().Be(tutorial);
        }
    }
}
