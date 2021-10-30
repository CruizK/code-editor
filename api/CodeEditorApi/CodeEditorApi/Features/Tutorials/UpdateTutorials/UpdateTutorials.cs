using CodeEditorApi.Errors;
using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.UpdateTutorials
{
    public interface IUpdateTutorials
    {
        public Task<ActionResult<Tutorial>> ExecuteAsync(int tutorialId, CreateTutorialsBody createTutorialsBody);
    }
    public class UpdateTutorials : IUpdateTutorials
    {
        private readonly CodeEditorApiContext _context;

        public UpdateTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<Tutorial>> ExecuteAsync(int tutorialId, CreateTutorialsBody createTutorialsBody)
        {
            var existingTutorial = await _context.Tutorials.FindAsync(tutorialId);
            if(existingTutorial != null)
            {
                _context.Entry(existingTutorial).CurrentValues.SetValues(createTutorialsBody);
                await _context.SaveChangesAsync();
            }

            return await _context.Tutorials.FindAsync(tutorialId);
        }
    }
}
