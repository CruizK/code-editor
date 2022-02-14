using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiDataAccess.StaticData;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
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
            var user = fixture.Build<User>()
               .With(u => u.RoleId, (int)Roles.Teacher)
               .Create();

            Freeze<IGetUser>().Setup(gu => gu.GetUserInfo(user.Id)).ReturnsAsync(user);
            Freeze<ICreateTutorials>().Setup(c => c.ExecuteAsync(user.Id, body)).ReturnsAsync(tutorial);

            var actionResult = await Target().ExecuteAsync(user.Id, body);

            actionResult.Should().NotBeNull();
            actionResult.Value.Should().Be(tutorial);
        }

        [Theory]
        [InlineData((int)Roles.Student)]
        [InlineData((int)Roles.Admin)]
        public async Task ShouldReturnBadRequestIfUserIsNotTeacher(int role)
        {
            var body = fixture.Create<CreateTutorialsBody>();
            var user = fixture.Build<User>()
                .With(u => u.RoleId, role)
                .Create();            

            var expected = new BadRequestError("User is not authorized to create tutorials.");
          
            Freeze<ICreateTutorials>().Setup(c => c.ExecuteAsync(user.Id, body)).ReturnsAsync((Tutorial)null);

            var actionResult = await Target().ExecuteAsync(user.Id, body);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task ShouldReturnBadRequestOnDuplicateTutorialTitle()
        {
            var courseId = fixture.Create<int>();
            var title = fixture.Create<string>();
            var user = fixture.Build<User>()
                .With(u => u.RoleId, (int)Roles.Teacher)
                .Create();

            var body = fixture.Build<CreateTutorialsBody>()
                .With(b => b.CourseId, courseId)
                .With(b => b.Title, title)
                .Create();

            var tutorials = fixture.Build<Tutorial>()
                .With(t => t.CourseId, courseId)
                .With(t => t.Title, title)
                .CreateMany()
                .ToList();


            var expected = new BadRequestError($"A tutorial already exists under course {courseId} with the same title '{title}'");

            Freeze<IGetTutorials>().Setup(g => g.GetCourseTutorials(courseId)).ReturnsAsync(tutorials);
            Freeze<IGetUser>().Setup(gu => gu.GetUserInfo(user.Id)).ReturnsAsync(user);
            Freeze<ICreateTutorials>().Setup(c => c.ExecuteAsync(user.Id, body)).ReturnsAsync((Tutorial)null);

            var actionResult = await Target().ExecuteAsync(user.Id, body);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }
    }
}
