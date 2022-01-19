using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.UnregisterUser
{
    public interface IUnregisterUserCommand
    {
        public Task<ActionResult<List<UserTutorial>>> ExecuteAsync(int courseId, int userId);
    }
    public class UnregisterUserCommand : IUnregisterUserCommand
    {
        private readonly IUnregisterUser _unregisterUser;

        public UnregisterUserCommand(IUnregisterUser unregisterUser)
        {
            _unregisterUser = unregisterUser;
        }
        public async Task<ActionResult<List<UserTutorial>>> ExecuteAsync(int courseId, int userId)
        {
            return await _unregisterUser.ExecuteAsync(courseId, userId);
        }
    }
}
