using CodeEditorApi.Features.Courses.DeleteCourses;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using FluentAssertions;
using Moq;
using System.Linq;
using CodeEditorApi.Errors;
using Microsoft.AspNetCore.Mvc;
using CodeEditorApi.Features.Courses.GetCourses;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class DeleteCoursesCommandTest : UnitTest<DeleteCoursesCommand>
    {
        [Fact]
        public async Task ShouldReturnBadRequestIfUserDidNotCreateCourse()
        {
            var user = fixture.Create<User>();
            var course = fixture.Create<Course>();
            var expected = new BadRequestError("Only the author of a course may delete it");

            Freeze<IDeleteCourses>().Setup(dc => dc.ExecuteAsync(course.Id)).ReturnsAsync((Course)null);

            var actionResult = await Target().ExecuteAsync(user.Id, course.Id);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);

        }

        [Fact]
        public async Task ShouldReturnDeletedCourse()
        {
            var user = fixture.Create<User>();
            var usercreatedCourses = fixture.Build<Course>()
                .With(c => c.Author, user.Id)
                .CreateMany().ToList();
            var course = usercreatedCourses.First();

            Freeze<IGetCourses>().Setup(gc => gc.GetUserCreatedCourses(user.Id)).ReturnsAsync(usercreatedCourses);
            Freeze<IDeleteCourses>().Setup(dc => dc.ExecuteAsync(course.Id)).ReturnsAsync(course);

            var actionResult = await Target().ExecuteAsync(user.Id, course.Id);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().Be(course);
        }
    }
}
