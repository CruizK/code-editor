using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Languages.GetLanguages
{
    public interface IGetProgrammingLanguages
    {
        public Task<ActionResult<List<CfgProgrammingLanguage>>> ExecuteAsync();
    }
    public class GetProgrammingLanguages : IGetProgrammingLanguages
    {
        private readonly CodeEditorApiContext _context;

        public GetProgrammingLanguages(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<CfgProgrammingLanguage>>> ExecuteAsync()
        {
            return await _context.CfgProgrammingLanguages.Select(pl => pl).ToListAsync();
        }
    }
}
