using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Levels
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly IGetDifficultyLevelsCommand _difficultyLevelsCommand;

        public LevelsController(IGetDifficultyLevelsCommand difficultyLevelsCommmand)
        {
            _difficultyLevelsCommand = difficultyLevelsCommmand;
        }
        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<List<CfgDifficultyLevel>>> GetLevels()
        {
            return await _difficultyLevelsCommand.ExecuteAsync();
        }
    }
}
