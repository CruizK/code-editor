using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApi.Features.Tutorials.CreateUserTutorials;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiDataAccess.StaticData;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
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
            var author = fixture.Build<User>()
                .With(a => a.RoleId, (int)Roles.Teacher)
                .Create();
            var course = fixture.Build<Course>()
                .With(c => c.Author, author.Id)
                .Create();
            var tutorials = fixture.Build<Tutorial>()
                .With(t => t.CourseId, course.Id)
                .With(t => t.Author, author.Id)
                .CreateMany();
            var users = fixture.CreateMany<int>()
                .ToList();
            var registeredUsersToTutorials = new List<UserTutorial>();


            foreach(var user in users)
            {
                fixture.Build<UserRegisteredCourse>()
                     .With(urc => urc.CourseId, course.Id)
                     .With(urc => urc.UserId, user)
                     .Create();
            }

            foreach(var tutorial in tutorials)
            {
                foreach(var user in users)
                {
                    var ut = new UserTutorial
                    {
                        TutorialId = tutorial.Id,
                        UserId = user
                    };
                    registeredUsersToTutorials.Add(ut);                    
                }
            }

            var ctb = fixture.Build<CreateTutorialsBody>()
                .With(ctb => ctb.CourseId, course.Id)
                .Create();
            var newtutorial = fixture.Build<Tutorial>()
                .With(nt => nt.CourseId, ctb.CourseId)
                .With(nt => nt.Author, author.Id)
                .Create();

            Freeze<IGetTutorials>().Setup(gt => gt.GetCourseTutorials(ctb.CourseId)).ReturnsAsync(tutorials.ToList());
            Freeze<IGetUser>().Setup(gu => gu.GetUserInfo(author.Id)).ReturnsAsync(author);
            Freeze<ICreateTutorials>().Setup(ct => ct.ExecuteAsync(author.Id, ctb)).ReturnsAsync(newtutorial);
            Freeze<IGetUser>().Setup(gu => gu.GetAllUsersRegisteredToCourse(ctb.CourseId)).ReturnsAsync(users);
            Freeze<ICreateUserTutorials>().Setup(cut => cut.ExecuteAsync(newtutorial.Id, users)).ReturnsAsync(users.Count);

            var actionResult = await Target().ExecuteAsync(author.Id, ctb);
            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().BeEquivalentTo(newtutorial);

        }

        [Fact]
        public async Task ShouldReturnBadRequestIfFailedToRegisterAllUsersUnderCourseToNewTutorial()
        {
            var author = fixture.Build<User>()
                .With(a => a.RoleId, (int)Roles.Teacher)
                .Create();
            var course = fixture.Build<Course>()
                .With(c => c.Author, author.Id)
                .Create();
            var tutorials = fixture.Build<Tutorial>()
                .With(t => t.CourseId, course.Id)
                .With(t => t.Author, author.Id)
                .CreateMany();
            var users = fixture.CreateMany<int>()
                .ToList();
            var registeredUsersToTutorials = new List<UserTutorial>();


            foreach (var user in users)
            {
                fixture.Build<UserRegisteredCourse>()
                     .With(urc => urc.CourseId, course.Id)
                     .With(urc => urc.UserId, user)
                     .Create();
            }

            foreach (var tutorial in tutorials)
            {
                foreach (var user in users)
                {
                    var ut = new UserTutorial
                    {
                        TutorialId = tutorial.Id,
                        UserId = user
                    };
                    registeredUsersToTutorials.Add(ut);
                }
            }

            var ctb = fixture.Build<CreateTutorialsBody>()
                .With(ctb => ctb.CourseId, course.Id)
                .Create();
            var newtutorial = fixture.Build<Tutorial>()
                .With(nt => nt.CourseId, ctb.CourseId)
                .With(nt => nt.Author, author.Id)
                .Create();

            var expected = new BadRequestError($"Failed to register Students under Course {ctb.CourseId} to new Tutorial");

            Freeze<IGetTutorials>().Setup(gt => gt.GetCourseTutorials(ctb.CourseId)).ReturnsAsync(tutorials.ToList());
            Freeze<IGetUser>().Setup(gu => gu.GetUserInfo(author.Id)).ReturnsAsync(author);
            Freeze<ICreateTutorials>().Setup(ct => ct.ExecuteAsync(author.Id, ctb)).ReturnsAsync(newtutorial);
            Freeze<IGetUser>().Setup(gu => gu.GetAllUsersRegisteredToCourse(ctb.CourseId)).ReturnsAsync(users);
            Freeze<ICreateUserTutorials>().Setup(cut => cut.ExecuteAsync(newtutorial.Id, users)).ReturnsAsync(It.IsNotIn<int>(users.Count));

            var actionResult = await Target().ExecuteAsync(author.Id, ctb);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);

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
