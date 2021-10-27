using CodeEditorApiDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.DeleteCourses
{
    public interface IDeleteTutorials
    {
        public void ExecuteAsync(int id);
    }
    public class DeleteTutorials : IDeleteTutorials
    {
        private readonly CodeEditorApiContext _context;
        public DeleteTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async void ExecuteAsync(int id)
        {
            var existingTutorial = await _context.Tutorials.FindAsync(id);
            if(existingTutorial != null)
            {
                _context.Tutorials.Remove(existingTutorial);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
