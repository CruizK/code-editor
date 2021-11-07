using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Levels
{
    public interface IGetDifficultyLevelsCommand
    {
        public Task<ActionResult<List<CfgDifficultyLevel>>> ExecuteAsync();
    }
    public class GetDifficultyLevelsCommand : IGetDifficultyLevelsCommand
    {
        private readonly IGetDifficultyLevels _difficultyLevels;
        
        public GetDifficultyLevelsCommand(IGetDifficultyLevels difficultyLevels)
        {
            _difficultyLevels = difficultyLevels;
        }

        public async Task<ActionResult<List<CfgDifficultyLevel>>> ExecuteAsync()
        {
            return await _difficultyLevels.ExecuteAsync();
        }
    }
}
