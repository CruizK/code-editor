﻿using CodeEditorApi.Errors;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.UpdateTutorials
{
    public interface IUpdateUserTutorialsCommand
    {
        public Task<ActionResult<UserTutorial>> ExecuteAsync(int tutorialId, int userId, UpdateUserTutorialBody updateUserTutorialBody);
    }
    public class UpdateUserTutorialsCommand : IUpdateUserTutorialsCommand
    {
        private IUpdateTutorials _updateTutorials;
        public UpdateUserTutorialsCommand(IUpdateTutorials updateTutorials)
        {
            _updateTutorials = updateTutorials;
        }

        public async Task<ActionResult<UserTutorial>> ExecuteAsync(int tutorialId, int userId, UpdateUserTutorialBody updateUserTutorialBody)
        {            
            if(updateUserTutorialBody.IsCompleted && updateUserTutorialBody.InProgress)
            {
                return ApiError.BadRequest($"Cannot submit a UserTutorial that is both in progress and completed.");
            }

            var result = await _updateTutorials.UpdateUserTutorial(tutorialId, userId, updateUserTutorialBody);

            return result;
        }
    }
}
