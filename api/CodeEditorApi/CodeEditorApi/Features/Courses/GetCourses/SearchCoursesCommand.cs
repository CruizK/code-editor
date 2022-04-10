using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CodeEditorApi.Features.Courses.GetCourses.GetCourses;

namespace CodeEditorApi.Features.Courses.GetCourses
{
    public interface ISearchCoursesCommand
    {
        public Task<ActionResult<List<SearchCoursesResponse>>> ExecuteAsync(string searchString, int difficultyId, int languageId);
    }
    public class SearchCoursesCommand : ISearchCoursesCommand
    {
        private readonly IGetCourses _getCourses;
        public SearchCoursesCommand(IGetCourses getCourses)
        {
            _getCourses = getCourses;
        }
        public async Task<ActionResult<List<SearchCoursesResponse>>> ExecuteAsync(string searchString, int difficultyId, int languageId)
        {
            var si = new SearchInput
            {
                searchString = searchString,
                difficultyId = difficultyId,
                languageId = languageId
            };

            if(searchString == null && difficultyId == 0 && languageId == 0)
            {
                return await _getCourses.GetAllPublishedCourses();
            }
                 

            return await _getCourses.SearchCourses(si);
        }
    }
}
