using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Languages.GetLanguages
{
    public interface IGetProgrammingLanguagesCommand
    {
        public Task<ActionResult<List<CfgProgrammingLanguage>>> ExecuteAsync();
    }
    public class GetProgrammingLanguagesCommand : IGetProgrammingLanguagesCommand
    {
        private readonly IGetProgrammingLanguages _programmingLanguages;

        public GetProgrammingLanguagesCommand(IGetProgrammingLanguages programmingLanguages)
        {
            _programmingLanguages = programmingLanguages;
        }

        public async Task<ActionResult<List<CfgProgrammingLanguage>>> ExecuteAsync()
        {
            return await _programmingLanguages.ExecuteAsync();
        }
    }
}
