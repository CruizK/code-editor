using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodeEditorApi.Errors;
using CodeEditorApiDataAccess.StaticData;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public interface IGetTutorials
    {
        public Task<ActionResult<Tutorial>> GetUserCreatedTutorial(int tutorialId);

        public Task<ActionResult<List<Tutorial>>> GetUserCreatedTutorialsList(int userId);

        public Task<List<Tutorial>> GetCourseTutorials(int courseId);

        public Task<Tutorial> GetUserLastInProgressTutorial(int userId, int courseId);

        public Task<List<UserTutorial>> GetUserRegisteredTutorials(int courseId, int userId);

        public Task<List<UserTutorial>> GetUserRegisteredTutorials(int userId);

        public Task<List<SearchTutorialsBody>> SearchTutorials(int courseId, SearchInput si);
    }
    public class GetTutorials : IGetTutorials
    {
        private readonly CodeEditorApiContext _context;

        public GetTutorials(CodeEditorApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// gets single tutorial - used for editing Tutorial
        /// </summary>
        /// <param name="tutorialId"></param>
        /// <returns></returns>
        public async Task<ActionResult<Tutorial>> GetUserCreatedTutorial(int tutorialId)
        {
            var tutorial = await _context.Tutorials.Where(t => t.Id == tutorialId).Select(t => t).FirstOrDefaultAsync();
            
            return tutorial;
        }

        /// <summary>
        /// gets all tutorials created by a User - not sure what this is used for
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ActionResult<List<Tutorial>>> GetUserCreatedTutorialsList(int courseId)
        {
            return await _context.Tutorials.Where(t => t.CourseId == courseId).ToListAsync();
        }

        /// <summary>
        /// gets all tutorials under a specific course - needed forvalidation
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<List<Tutorial>> GetCourseTutorials(int courseId)
        {
            return await _context.Tutorials.Where(t => t.IsPublished && t.CourseId == courseId)
                .Include(c => c.Difficulty)
                .Include(c => c.Language)
                .Select(t => t).ToListAsync();
        }
        
        /// <summary>
        /// gets all tutorials under a course 
        /// and then looks for user's progress 
        /// based on those tutorial ids 
        /// - used for a Course page
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserTutorial>> GetUserRegisteredTutorials(int courseId, int userId)
        {
            var courseTutorials = await _context.Tutorials.Where(t => t.CourseId == courseId).Select(t => t.Id).ToListAsync();
            var inProgressTutorials = await _context.UserTutorials
                .Where(ut => courseTutorials.Contains(ut.TutorialId) && ut.UserId == userId)
                .OrderByDescending(t => t.ModifyDate)
                .Select(ut => ut).ToListAsync();

            return inProgressTutorials;
        }       

        /// <summary>
        /// gets all the tutorials a User is registered to
        /// used for validation on GetUserLastInProgressTutorialCommand
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserTutorial>> GetUserRegisteredTutorials(int userId)
        {
            return await _context.UserTutorials.Where(t => t.UserId == userId).Select(t => t).ToListAsync();
        }


        /// <summary>
        /// gets the tutorials that a user is currently working on based on a course id
        /// then returns the tutorial that was modified most recently
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<Tutorial> GetUserLastInProgressTutorial(int userId, int courseId)
        {
            var courseTutorials = await _context.Tutorials.Where(t => t.CourseId == courseId).Select(t => t.Id).ToListAsync();
            var userTutorials = await _context.UserTutorials.Where(ut => courseTutorials.Contains(ut.TutorialId) && ut.UserId == userId).Select(o => o).ToListAsync();

            var inProgressTutorialId = userTutorials.Where(ut => ut.Status == (int)TutorialStatus.InProgress).OrderByDescending(t => t.ModifyDate).Select(ut => ut.TutorialId).FirstOrDefault();

            var tutorial = await _context.Tutorials.Where(t => t.Id == inProgressTutorialId).Select(t => t).FirstOrDefaultAsync();

            return tutorial;
        }

        public async Task<List<SearchTutorialsBody>> SearchTutorials(int courseId, SearchInput si)
        {
            var tutorials = await _context.Tutorials
                            .Where(t => t.CourseId == courseId && t.IsPublished)
                            .Select(t => new SearchTutorialsBody
                            {
                                Title = t.Title,
                                DifficultyId = t.DifficultyId,
                                LanguageId = t.LanguageId
                            }).ToListAsync();

            var query = new List<SearchTutorialsBody>();
            if (si.searchString != null)
            {
                query = tutorials.Where(t => t.LanguageId == si.languageId
                                || t.DifficultyId == si.difficultyId
                                || t.Title.Contains(si.searchString)
                                )
                        .OrderByDescending(t => t.LanguageId == si.languageId)
                        .ThenByDescending(t => t.DifficultyId == si.difficultyId)
                        .ThenByDescending(t => t.Title.Contains(si.searchString))
                        .Select(t => new SearchTutorialsBody
                        {
                            Title = t.Title,
                            LanguageId = t.LanguageId,
                            DifficultyId = t.DifficultyId
                        })
                        .ToList();
            }
            // else if the search string is empty, but at least a diff/lang has been chosen, then search only for those 2 criteria
            // (passing null search string would return bad request error)
            else if (si.languageId > 0 || si.difficultyId > 0)
            {
                query = tutorials.Where(t => t.LanguageId == si.languageId
                                || t.DifficultyId == si.difficultyId)
                        .OrderByDescending(t => t.LanguageId == si.languageId)
                        .ThenByDescending(t => t.DifficultyId == si.difficultyId)
                        .Select(t => new SearchTutorialsBody
                        {
                            Title = t.Title,
                            LanguageId = t.LanguageId,
                            DifficultyId = t.DifficultyId
                        })
                        .ToList();
            }
            //else return all published tutorials under that course
            else
            {
                return tutorials;
            }

            return query;
        }
    }
}
