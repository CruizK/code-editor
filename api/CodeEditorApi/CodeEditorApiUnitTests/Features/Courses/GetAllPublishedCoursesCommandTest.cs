using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class GetAllPublishedCoursesCommandTest : UnitTest<GetAllPublishedCoursesCommand>
    {
        [Fact]
        public async Task ShouldReturnListOfAllPublishedCourses()
        {
            var publishedCourses = fixture.Build<Course>()
                .With(c => c.IsPublished, true)
                .CreateMany();

            Freeze<IGetCourses>().Setup(gc => gc.GetAllPublishedCourses()).ReturnsAsync(publishedCourses.ToList());

            var actionResult = await Target().GetAllPublishedCourses();

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().BeEquivalentTo(publishedCourses);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfNoPublishedCourses()
        {
            var expected = ApiError.BadRequest("No published courses found");            

            Freeze<IGetCourses>().Setup(gc => gc.GetAllPublishedCourses()).ReturnsAsync(new List<Course>());

            var actionResult = await Target().GetAllPublishedCourses();

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected as BadRequestObjectResult);

        }

        [Fact]
        public async Task ShouldReturnListOfAllPublishedCoursesSortedByModifyDate()
        {
            var publishedCourses = fixture.Build<Course>()
                .With(c => c.IsPublished, true)
                .CreateMany();

            Freeze<IGetCourses>().Setup(gc => gc.GetAllPublishedCoursesSortByModifyDate()).ReturnsAsync(publishedCourses.ToList());

            var actionResult = await Target().GetAllPublishedCoursesSortByModifyDate();

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().BeEquivalentTo(publishedCourses);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfNoPublishedCoursesSortedByModifyDate()
        {
            var expected = ApiError.BadRequest("No published courses found");

            Freeze<IGetCourses>().Setup(gc => gc.GetAllPublishedCoursesSortByModifyDate()).ReturnsAsync(new List<Course>());

            var actionResult = await Target().GetAllPublishedCoursesSortByModifyDate();

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected as BadRequestObjectResult);

        }
    }
}
