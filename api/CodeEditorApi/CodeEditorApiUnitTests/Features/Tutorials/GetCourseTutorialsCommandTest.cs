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
    public class GetCourseTutorialsCommandTest : UnitTest<GetCourseTutorialsCommand>
    {
        [Fact]
        public async Task ShouldReturnTutorialsWithGivenCourseId()
        {
            var course = fixture.Create<Course>();
            var tutorials = fixture.Build<Tutorial>()
                .With(t => t.CourseId, course.Id).CreateMany().ToList();

            Freeze<IGetTutorials>().Setup(g => g.GetUserCreatedTutorialsList(course.Id)).ReturnsAsync(tutorials);

            var actionResult = await Target().ExecuteAsync(course.Id);

            actionResult.Value.Should().BeEquivalentTo(tutorials);
        }
    }
}
