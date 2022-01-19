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
    public class GetMostPopularCoursesTest : UnitTest<GetMostPopularCoursesCommand>
    {
        [Fact]
        public async Task ShouldReturnListOfMostPopularCourseIds()
        {
            var courses = fixture.Build<Course>()
                .CreateMany()
                .ToList();


            Freeze<IGetCourses>().Setup(gc => gc.GetMostPopularCourses()).ReturnsAsync(courses);

            var actionresult = await Target().ExecuteAsync();

            actionresult.Result.Should().BeNull();
            actionresult.Value.Should().BeEquivalentTo(courses);
        }

    }
}
