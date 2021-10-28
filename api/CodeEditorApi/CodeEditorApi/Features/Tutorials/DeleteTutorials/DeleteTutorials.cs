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
        public Task<ActionResult<Tutorial>> ExecuteAsync(int id);
    }
    public class DeleteTutorials : IDeleteTutorials
    {
        private readonly CodeEditorApiContext _context;
        public DeleteTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Tutorial>> ExecuteAsync(int id)
        {
            var existingTutorial = await _context.Tutorials.FindAsync(id);
            if(existingTutorial != null)
            {
                _context.Tutorials.Remove(existingTutorial);
                if (await _context.SaveChangesAsync() != 1)
                {
                    return ApiError.BadRequest($"Unable to delete tutorial with id {id}");
                }                
            }

            return existingTutorial;
            
        }
    }
}
