using AutoFixture;
using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class CreateTutorialsCommandTest : UnitTest<CreateTutorialsCommand>
    {
        [Fact]
        public async Task ShouldReturnTutorial()
        {
            var body = fixture.Create<CreateTutorialsBody>();
            var tutorial = fixture.Create<Tutorial>();

            Freeze<ICreateTutorials>().Setup(c => c.ExecuteAsync(body)).ReturnsAsync(tutorial);

            var actionResult = await Target().ExecuteAsync(body);

            actionResult.Should().NotBeNull();
            actionResult.Value.Should().Be(tutorial);
        }
    }
}
