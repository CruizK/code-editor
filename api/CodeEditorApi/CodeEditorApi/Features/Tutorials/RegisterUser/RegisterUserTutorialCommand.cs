using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.RegisterUser
{
    public interface IRegisterUserTutorialCommand
    {
        public Task<ActionResult<List<Tutorial>>> ExecuteAsync(int userId, List<Tutorial> tutorials);
    }
    public class RegisterUserTutorialCommand : IRegisterUserTutorialCommand
    {
        private readonly IRegisterUserTutorial _registerUserTutorial;

        public RegisterUserTutorialCommand(IRegisterUserTutorial registerUserTutorial)
        {
            _registerUserTutorial = registerUserTutorial;
        }
        public async Task<ActionResult<List<Tutorial>>> ExecuteAsync(int userId, List<Tutorial> tutorials)
        {
            return await _registerUserTutorial.ExecuteAsync(userId, tutorials);
        }
    }
}
