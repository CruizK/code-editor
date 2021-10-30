using CodeEditorApi.Errors;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.DeleteTutorials
{
    public interface IDeleteTutorials
    {
        public Task<ActionResult<Tutorial>> ExecuteAsync(int tutorialId);
    }
    public class DeleteTutorials : IDeleteTutorials
    {
        private readonly CodeEditorApiContext _context;
        public DeleteTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Tutorial>> ExecuteAsync(int tutorialId)
        {
            var existingTutorial = await _context.Tutorials.FindAsync(tutorialId);
            if(existingTutorial != null)
            {
                _context.Tutorials.Remove(existingTutorial);
                await _context.SaveChangesAsync();            
            }

            return existingTutorial;
            
        }
    }
}
