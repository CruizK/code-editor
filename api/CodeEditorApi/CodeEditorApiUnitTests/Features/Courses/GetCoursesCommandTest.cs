using AutoFixture;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class GetCoursesCommandTest : UnitTest<GetCoursesCommand>
    {
        [Fact]
        public async Task ShouldReturnUserRegisteredCourses()
        {
            var user = fixture.Create<User>();
            var courses = fixture.CreateMany<Course>().ToList();

            var userRegisteredCourses = new List<UserRegisteredCourse>();

            foreach(var course in courses)
            {
                var userRegisteredCourse = fixture.Build<UserRegisteredCourse>().With(urc => urc.UserId, user.Id).With(urc => urc.CourseId, course.Id).Create();
                userRegisteredCourses.Add(userRegisteredCourse);
            }   
            
            Freeze<IGetCourses>().Setup(gc => gc.GetUserCourses(user.Id)).ReturnsAsync(courses);

            var actionResult = await Target().ExecuteAsync(user.Id);

            actionResult.Value.Should().BeEquivalentTo(courses);


        }
    }
}