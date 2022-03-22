using CodeEditorApi.Features;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using AutoFixture;
using Moq;
using FluentAssertions;

namespace CodeEditorApiUnitTests.Features.Courses
{
    public class SearchCoursesCommandTest : UnitTest<SearchCoursesCommand>
    {
        [Theory]
        [InlineData("Course", 1, 1)]
        [InlineData("Course", 1, 0)]
        [InlineData("Course", 0, 1)]
        [InlineData("Course", 0, 0)]
        [InlineData("", 0, 0)]
        public async Task ShouldReturnCourseList(string searchString, int DID, int LID)
        {
            var si = fixture.Create<SearchInput>();

            var courseList = fixture.Create<List<Course>>();

            Freeze<IGetCourses>().Setup(gc => gc.SearchCourses(si)).ReturnsAsync(courseList);

            var actionResult = await Target().ExecuteAsync(searchString, DID, LID);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().NotBeEmpty();
        }
    }
}
