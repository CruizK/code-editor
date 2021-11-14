using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeEditorApi.Features.Levels
{
    public interface IGetDifficultyLevels
    {
        public Task<ActionResult<List<CfgDifficultyLevel>>> ExecuteAsync();
    }
    public class GetDifficultyLevels : IGetDifficultyLevels
    {
        private readonly CodeEditorApiContext _context;
        public GetDifficultyLevels(CodeEditorApiContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<List<CfgDifficultyLevel>>> ExecuteAsync()
        {
            return await _context.CfgDifficultyLevels.Select(dl => dl).ToListAsync();

        }
    }
}
