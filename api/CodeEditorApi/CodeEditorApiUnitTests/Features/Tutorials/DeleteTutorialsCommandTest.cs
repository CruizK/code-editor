using AutoFixture;
using CodeEditorApi.Features.Tutorials.DeleteTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class DeleteTutorialsCommandTest : UnitTest<DeleteTutorialsCommand>
    {
        [Fact]
        public async Task ShouldReturnTutorial()
        {
            var tutorial = fixture.Create<Tutorial>();

            Freeze<IDeleteTutorials>().Setup(d => d.ExecuteAsync(tutorial.Id)).ReturnsAsync(tutorial);

            var actionResult = await Target().ExecuteAsync(tutorial.Id);

            actionResult.Value.Should().Be(tutorial);
        }
    }
}
