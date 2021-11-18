using CodeEditorApi.Features.Languages.GetLanguages;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Languages
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly IGetProgrammingLanguagesCommand _getProgrammingLanguagesCommand;

        public LanguagesController(IGetProgrammingLanguagesCommand getProgrammingLanguagesCommand)
        {
            _getProgrammingLanguagesCommand = getProgrammingLanguagesCommand;
        }

        /// <summary>
        /// Gets the programming languages available for tutorials
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CfgProgrammingLanguage>>> GetLanguages()
        {
            return await _getProgrammingLanguagesCommand.ExecuteAsync();
        }
    }
}
