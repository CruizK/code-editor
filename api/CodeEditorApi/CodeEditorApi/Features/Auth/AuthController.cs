﻿using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.Login;
using CodeEditorApi.Features.Auth.Register;
using CodeEditorApi.Features.Auth.UpdateUser;
using CodeEditorApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IRegisterCommand _registerCommand;
        private readonly ILoginCommand _loginCommand;
        private readonly IEmailService _emailService;
        private readonly IUpdateUserCommand _updateUserCommand;
        private readonly IWebHostEnvironment _enviornment;

        public AuthController(IRegisterCommand registerCommand, ILoginCommand loginCommand, IEmailService emailService, IUpdateUserCommand updateUserCommand, IWebHostEnvironment environment)
        {
            _registerCommand = registerCommand;
            _loginCommand = loginCommand;
            _emailService = emailService;
            _updateUserCommand = updateUserCommand;
            _enviornment = environment;
        }

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="registerBody"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Register([FromBody] RegisterBody registerBody)
        {
            if(_enviornment.EnvironmentName == "Staging")
            {
                return ApiError.BadRequest("Route is disabled in staging");
            }
            return await _registerCommand.ExecuteAsync(registerBody);
        }

        /// <summary>
        /// Logs a user in
        /// </summary>
        /// <param name="loginBody"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] LoginBody loginBody)
        {
            return await _loginCommand.ExecuteAsync(loginBody);
        }

        /// <summary>
        /// Generates access code, only for admins
        /// </summary>
        /// <param name="roleRequestBody"></param>
        /// <returns>string</returns>
        [HttpPost("GenerateAccessCode")]
        [Authorize(Roles = "Admin")]
        public Task<string> GenerateAccessCode([FromBody] RoleRequestBody body)
        {
            return Task.FromResult(AccessCodeService.GenerateAccessCode(body.Role));
        }
                
        [HttpPut("UpdateUser")]
        [Authorize]
        public async Task<ActionResult<string>> UpdateUser([FromBody] UpdateUserBody updateUserBody)
        {
            var userId = HttpContextHelper.retrieveRequestUserId(HttpContext);
            updateUserBody.Id = userId;
            return await _updateUserCommand.ExecuteAsync(updateUserBody);
        }
    }
}