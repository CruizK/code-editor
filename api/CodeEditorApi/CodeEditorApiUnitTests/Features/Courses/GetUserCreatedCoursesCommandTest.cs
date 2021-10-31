using AutoFixture;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class GetUserCreatedCoursesCommandTest : UnitTest<GetUserCreatedCoursesCommand>
    {
        [Fact]
        public async Task ShouldReturnUserCreatedCourses()
        {
            var user = fixture.Create<User>();
            var courses = fixture.Build<Course>().With(c => c.Author, user.Id)
                .CreateMany().ToList();

            Freeze<IGetCourses>().Setup(gc => gc.GetUserCreatedCourses(user.Id)).ReturnsAsync(courses);

            var actionResult = await Target().ExecuteAsync(user.Id);

            actionResult.Value.Should().BeEquivalentTo(courses);
        }
    }
}
