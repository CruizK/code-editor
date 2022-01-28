using CodeEditorApi.Features.Auth.CreateAccessCode;
using CodeEditorApi.Features.Auth.GetAccess;
using CodeEditorApi.Features.Auth.Login;
using CodeEditorApi.Features.Auth.Register;
using CodeEditorApi.Services;
using CodeEditorApiDataAccess.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IRegisterCommand _registerCommand;
        private readonly ILoginCommand _loginCommand;

        public AuthController(IRegisterCommand registerCommand, ILoginCommand loginCommand)
        {
            _registerCommand = registerCommand;
            _loginCommand = loginCommand;
        }

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="registerBody"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<ActionResult<string>> RegisterTeacher([FromBody] RegisterBody registerBody)
        {
            return await _registerCommand.ExecuteAsync(registerBody);
        }

        /// <summary>
        /// Logs a user in
        /// </summary>
        /// <param name="loginBody"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginBody loginBody)
        {
            return await _loginCommand.ExecuteAsync(loginBody);
        }

        [HttpPost("GenerateAccessCode")]
        [Authorize(Roles = "Admin")]
        public Task<string> GenerateAccessCode([FromBody] int RoleID)
        {
            return Task.FromResult(AccessCodeService.GenerateAccessCode((Roles)RoleID));
        }
    }
}