using AutoFixture;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class GetCoursesCommandTest
    {

        private readonly GetCoursesCommand _target;
        private readonly Fixture _fixture;

        private readonly Mock<IGetCourses> _getCoursesMock;

        public GetCoursesCommandTest()
        {
            _getCoursesMock = new Mock<IGetCourses>();
            _fixture = new Fixture();
            _target = new GetCourseCommand(_getCoursesMock.Object);
        }

        [Fact]
        public async Task ShouldReturnCourses()
        {
            // Assemble
            var expected = _fixture.CreateMany<Course>();
            
            _getCoursesMock.Setup(x => x.ExecuteAsync())
                .ReturnsAsync(expected);

            // Act
            var result = await _target.ExecuteAsync();

            result.Should().BeEquivalentTo(expected);
        }
    }
}
