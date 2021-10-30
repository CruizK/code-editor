using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeEditorApi.Errors;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace CodeEditorApi.Features.Tutorials.CreateTutorials
{
    public interface ICreateTutorials
    {
        public Task<ActionResult<Tutorial>> ExecuteAsync(CreateTutorialsBody getTutorialsBody);
    }
    public class CreateTutorials : ICreateTutorials
    {
        private readonly CodeEditorApiContext _context;

        public CreateTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<Tutorial>> ExecuteAsync(CreateTutorialsBody createTutorialsBody)
        {
            var insertTutorial = new Tutorial
            {
                CourseId = createTutorialsBody.CourseId,
                Title = createTutorialsBody.Title,
                Author = createTutorialsBody.Author,
                Description = createTutorialsBody.Description,
                IsPublished = createTutorialsBody.IsPublished,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                LanguageId = createTutorialsBody.LanguageId,
                DifficultyId = createTutorialsBody.DifficultyId
                //TODO: determine what datatype to use for prompt
            };

            await _context.Tutorials.AddAsync(insertTutorial);
            await _context.SaveChangesAsync();
                
            return insertTutorial;
        }
    }
}
