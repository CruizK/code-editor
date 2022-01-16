using CodeEditorApi.Features.Courses.CreateCourses;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using Xunit;
using AutoFixture;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using CodeEditorApi.Errors;

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

        [Fact]
        public async Task ShouldReturnBadRequestErrorIfTitleIsEmpty()
        {
            var user = fixture.Create<User>();

            var body = fixture.Build<CreateCourseBody>()
                .With(b => b.Title, "")
                .Create();            

            var expected = new BadRequestError("Unable to create course with no Title.");

            Freeze<ICreateCourses>().Setup(cc => cc.ExecuteAsync(It.IsAny<Course>())).ReturnsAsync((Course)null);

            var actionResult = await Target().ExecuteAsync(user.Id, body);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }
    }
}
