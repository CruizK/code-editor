using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface ISearchTutorialsCommand
    {
        Task<ActionResult<List<SearchTutorialsBody>>> ExecuteAsync(int courseId, string searchString, int difficultyId, int languageId);
    }
    public class SearchTutorialsCommand : ISearchTutorialsCommand
    {
        private readonly IGetTutorials _getTutorials;

        public SearchTutorialsCommand(IGetTutorials getTutorials)
        {
            _getTutorials = getTutorials;
        }
        public async Task<ActionResult<List<SearchTutorialsBody>>> ExecuteAsync(int courseId, string searchString, int difficultyId, int languageId)
        {
            var si = new SearchInput
            {
                searchString = searchString,
                difficultyId = difficultyId,
                languageId = languageId
            };
            
            return await _getTutorials.SearchTutorials(courseId, si);
        }
    }
}
