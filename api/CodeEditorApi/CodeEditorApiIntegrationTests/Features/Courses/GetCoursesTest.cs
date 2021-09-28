using AutoFixture;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiIntegrationTests.Features.Courses
{
    public class GetCoursesTest : DbTest
    {
        public GetCoursesTest() : base()
        {

        }

        [Fact]
        public async Task ShouldGetCoursesFromDB()
        {
            using(var context = TestContext())
            {
                // Assemble
                var getCourses = new GetCourse(context);
                var courses = _fixture.CreateMany<Course>();
                await context.AddRangeAsync(courses);
                await context.SaveChangesAsync();

                // Act
                var result = await getCourses.ExecuteAsync();

                // Assert
                result.Should().BeEquivalentTo(courses);
            }
        }
    }
}
