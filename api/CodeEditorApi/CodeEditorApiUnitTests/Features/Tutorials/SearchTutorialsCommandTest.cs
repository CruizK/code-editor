using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using CodeEditorApi.Features;
using Moq;
using System.Collections.Generic;
using FluentAssertions;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class SearchTutorialsCommandTest : UnitTest<SearchTutorialsCommand>
    {
        [Theory]
        [InlineData("Course", 1, 1)]
        [InlineData("Course", 1, 0)]
        [InlineData("Course", 0, 1)]
        [InlineData("Course", 0, 0)]
        [InlineData("",0,0)]
        public async Task ShouldReturnSearchTutorialBodyList(string searchString, int DID, int LID)
        {
            var body = fixture.Build<SearchInput>()
                .With(b => b.searchString, searchString)
                .With(b => b.difficultyId, DID)
                .With(b => b.languageId, LID)
                .Create();

            var courseId = It.IsAny<int>();

            var response = fixture.Create<List<SearchTutorialsBody>>();

            Freeze<IGetTutorials>().Setup(gt => gt.SearchTutorials(courseId, body))
                .ReturnsAsync(response);

            var actionResult = await Target().ExecuteAsync(courseId, searchString, DID, LID);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().NotBeEmpty();
        }
    }
}
