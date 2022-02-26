using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Courses.GetCourses;
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
    public class GetUserLastInProgressTutorialCommandTest : UnitTest<GetUserLastInProgressTutorialCommand>
    {
        [Fact]
        public async Task ShouldReturnBadRequestIfUserNotRegisteredForCourse()
        {
            //user is not registered to a course or its tutorials, has created no content
            var user = fixture.Build<User>()
                .Without(u => u.UserRegisteredCourses)
                .Without(u => u.Courses)
                .Without(u => u.Tutorials)
                .Without(u => u.UserTutorials)
                .Create();

            var expected = ApiError.BadRequest($"User is not registered for course with id {It.IsAny<int>()}");

            Freeze<IGetCourses>().Setup(gc => gc.GetUserCourses(user.Id)).ReturnsAsync(new List<Course>());

            var actionResult = await Target().ExecuteAsync(user.Id, It.IsAny<int>());

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected as BadRequestObjectResult);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfUserHasNoInProgressTutorial()
        {

            //user is not registered to a course or its tutorials, has created no content
            var user = fixture.Build<User>()
                .Without(u => u.UserRegisteredCourses)
                .Without(u => u.Courses)
                .Without(u => u.Tutorials)
                .Without(u => u.UserTutorials)
                .Create();

            //the user that will create content for course and tutorial (separate from user who only consumes content)
            var userId = fixture.Create<int>();

            //course has no users registered to it
            var course = fixture.Build<Course>()
                .With(c => c.Author, userId)
                .Without(c => c.UserRegisteredCourses)
                .Without(c => c.Tutorials)
                .Create();

            //tutorials 
            var tutorials = fixture.Build<Tutorial>()
                .With(t => t.Author, userId)
                .With(t => t.CourseId, course.Id)
                .Without(t => t.Course)
                .Without(t => t.UserTutorials)
                .Without(t => t.AuthorNavigation)
                .Without(t => t.Difficulty)
                .Without(t => t.Language)
                .CreateMany();

            course.Tutorials = tutorials.ToList();

            //add course to user
            var userRegisteredCourse = fixture.Build<UserRegisteredCourse>()
                .With(urc => urc.CourseId, course.Id)
                .With(urc => urc.UserId, user.Id)
                .Create();

            user.UserRegisteredCourses.Add(userRegisteredCourse);

            //add userTutorials to User
            foreach (var t in tutorials)
            {
                var ut = new UserTutorial
                {
                    UserId = user.Id,
                    TutorialId = t.Id,
                    Status = (int)TutorialStatus.NotStarted
                };

                user.UserTutorials.Add(ut);
            }

            var userCourseList = new List<Course>();
            userCourseList.Add(course);

            var expected = ApiError.BadRequest($"User has not started any tutorial for course with id {course.Id}");

            Freeze<IGetCourses>().Setup(gc => gc.GetUserCourses(user.Id)).ReturnsAsync(userCourseList);
            Freeze<IGetTutorials>().Setup(gt => gt.GetUserRegisteredTutorials(user.Id)).ReturnsAsync((List<UserTutorial>)null);

            var actionResult = await Target().ExecuteAsync(user.Id, course.Id);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected as BadRequestObjectResult);


        }

        [Fact]
        public async Task ShouldReturnBadRequestIfInProgressTutorialDetailsAreNotFound()
        {
            //user is not registered to a course or its tutorials, has created no content
            var user = fixture.Build<User>()
                .Without(u => u.UserRegisteredCourses)
                .Without(u => u.Courses)
                .Without(u => u.Tutorials)
                .Without(u => u.UserTutorials)
                .Create();

            //the user that will create content for course and tutorial (separate from user who only consumes content)
            var userId = fixture.Create<int>();

            //course has no users registered to it
            var course = fixture.Build<Course>()
                .With(c => c.Author, userId)
                .Without(c => c.UserRegisteredCourses)
                .Without(c => c.Tutorials)
                .Create();

            //tutorials 
            var tutorials = fixture.Build<Tutorial>()
                .With(t => t.Author, userId)
                .With(t => t.CourseId, course.Id)
                .Without(t => t.Course)
                .Without(t => t.UserTutorials)
                .Without(t => t.AuthorNavigation)
                .Without(t => t.Difficulty)
                .Without(t => t.Language)
                .CreateMany();

            course.Tutorials = tutorials.ToList();

            //add course to user
            var userRegisteredCourse = fixture.Build<UserRegisteredCourse>()
                .With(urc => urc.CourseId, course.Id)
                .With(urc => urc.UserId, user.Id)
                .Create();
                  
            user.UserRegisteredCourses.Add(userRegisteredCourse);

            //add userTutorials to User
            foreach (var t in tutorials)
            {
                var ut = new UserTutorial
                {
                    UserId = user.Id,
                    TutorialId = t.Id,
                    Status = (int)TutorialStatus.NotStarted
                };

                user.UserTutorials.Add(ut);
            }

            user.UserTutorials.First().Status = (int)TutorialStatus.InProgress;

            var userCourseList = new List<Course>();
            userCourseList.Add(course);

            var expected = ApiError.BadRequest($"In Progress Tutorial could not be found.");

            Freeze<IGetCourses>().Setup(gc => gc.GetUserCourses(user.Id)).ReturnsAsync(userCourseList);
            Freeze<IGetTutorials>().Setup(gt => gt.GetUserRegisteredTutorials(user.Id)).ReturnsAsync(user.UserTutorials.ToList());
            Freeze<IGetTutorials>().Setup(gt => gt.GetUserLastInProgressTutorial(user.Id, course.Id)).ReturnsAsync((Tutorial)null);

            var actionResult = await Target().ExecuteAsync(user.Id, course.Id);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected as BadRequestObjectResult);
        }

        [Fact]
        public async Task ShouldReturnUserInProgressTutorial()
        {
            //user is not registered to a course or its tutorials, has created no content
            var user = fixture.Build<User>()
                .Without(u => u.UserRegisteredCourses)
                .Without(u => u.Courses)
                .Without(u => u.Tutorials)
                .Without(u => u.UserTutorials)
                .Create();

            //the user that will create content for course and tutorial (separate from user who only consumes content)
            var userId = fixture.Create<int>();

            //course has no users registered to it
            var course = fixture.Build<Course>()
                .With(c => c.Author, userId)
                .Without(c => c.UserRegisteredCourses)
                .Without(c => c.Tutorials)
                .Create();

            //tutorials 
            var tutorials = fixture.Build<Tutorial>()
                .With(t => t.Author, userId)
                .With(t => t.CourseId, course.Id)
                .Without(t => t.Course)
                .Without(t => t.UserTutorials)
                .Without(t => t.AuthorNavigation)
                .Without(t => t.Difficulty)
                .Without(t => t.Language)
                .CreateMany();

            course.Tutorials = tutorials.ToList();

            //add course to user
            var userRegisteredCourse = fixture.Build<UserRegisteredCourse>()
                .With(urc => urc.CourseId, course.Id)
                .With(urc => urc.UserId, user.Id)
                .Create();

            user.UserRegisteredCourses.Add(userRegisteredCourse);

            //add userTutorials to User
            foreach (var t in tutorials)
            {
                var ut = new UserTutorial
                {
                    UserId = user.Id,
                    TutorialId = t.Id,
                    Status = (int)TutorialStatus.NotStarted
                };

                user.UserTutorials.Add(ut);
            }

            user.UserTutorials.First().Status = (int)TutorialStatus.InProgress;

            var userCourseList = new List<Course>();
            userCourseList.Add(course);

            Freeze<IGetCourses>().Setup(gc => gc.GetUserCourses(user.Id)).ReturnsAsync(userCourseList);
            Freeze<IGetTutorials>().Setup(gt => gt.GetUserRegisteredTutorials(user.Id)).ReturnsAsync(user.UserTutorials.ToList());
            Freeze<IGetTutorials>().Setup(gt => gt.GetUserLastInProgressTutorial(user.Id, course.Id)).ReturnsAsync(tutorials.First());

            var actionResult = await Target().ExecuteAsync(user.Id, course.Id);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().BeEquivalentTo(tutorials.First());
        }
    }
}
