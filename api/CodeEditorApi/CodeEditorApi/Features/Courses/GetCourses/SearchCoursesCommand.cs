using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface ISearchCoursesCommand
    {
        public Task<ActionResult<List<Course>>> ExecuteAsync(string searchString, int difficultyId, int languageId);
    }
    public class SearchCoursesCommand : ISearchCoursesCommand
    {
        private readonly IGetCourses _getCourses;
        public SearchCoursesCommand(IGetCourses getCourses)
        {
            _getCourses = getCourses;
        }
        public async Task<ActionResult<List<Course>>> ExecuteAsync(string searchString, int difficultyId, int languageId)
        {
            var si = new SearchInput
            {
                searchString = searchString,
                difficultyId = difficultyId,
                languageId = languageId
            };

            return await _getCourses.SearchCourses(si);
        }
    }
}
