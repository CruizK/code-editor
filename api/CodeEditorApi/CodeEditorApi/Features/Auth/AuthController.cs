﻿using CodeEditorApi.Features.Auth.Login;
using CodeEditorApi.Features.Auth.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("Register/Student")]
        public async Task<ActionResult<string>> RegisterUser([FromBody] RegisterBody registerBody)
        {
            return await _registerCommand.ExecuteAsync(registerBody, CodeEditorApiDataAccess.StaticData.Roles.Student);
        }

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="registerBody"></param>
        /// <returns></returns>
        [HttpPost("Register/Teacher")]
        public async Task<ActionResult<string>> RegisterTeacher([FromBody] RegisterBody registerBody)
        {
            return await _registerCommand.ExecuteAsync(registerBody, CodeEditorApiDataAccess.StaticData.Roles.Teacher);
        }

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="registerBody"></param>
        /// <returns></returns>
        [HttpPost("Register/Admin")]
        public async Task<ActionResult<string>> RegisterAdmin([FromBody] RegisterBody registerBody)
        {
            return await _registerCommand.ExecuteAsync(registerBody, CodeEditorApiDataAccess.StaticData.Roles.Admin);
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
    }
}