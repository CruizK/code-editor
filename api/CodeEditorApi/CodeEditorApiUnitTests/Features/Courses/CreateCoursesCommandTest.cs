using CodeEditorApi.Features.Courses.CreateCourses;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using Xunit;
using AutoFixture;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class CreateCoursesCommandTest : UnitTest<CreateCoursesCommand>
    {
        [Fact]
        public async Task ShouldReturnCreatedCourse()
        {
            var user = fixture.Create<User>();

            var body = fixture.Build<CreateCourseBody>()
                .With(b => b.Title, "ValidTitle")
                .Create();        
            
            var course = fixture.Build<Course>()
                .With(c => c.Author, user.Id)
                .With(c => c.Title, body.Title)
                .With(c => c.Description, body.Description)
                .With(c => c.IsPublished, body.IsPublished)
                .Create();

            Freeze<ICreateCourses>().Setup(cc => cc.ExecuteAsync(It.IsAny<Course>())).ReturnsAsync(course);

            var actionResult = await Target().ExecuteAsync(user.Id, body);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().Be(course);
        }
    }
}
