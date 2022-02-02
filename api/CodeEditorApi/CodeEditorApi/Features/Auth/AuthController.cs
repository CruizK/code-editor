using CodeEditorApi.Features.Auth.Login;
using CodeEditorApi.Features.Auth.Register;
using CodeEditorApi.Services;
using Microsoft.AspNetCore.Authorization;
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

        public AuthController(IRegisterCommand registerCommand, ILoginCommand loginCommand, IEmailService emailService)
        {
            _registerCommand = registerCommand;
            _loginCommand = loginCommand;
            _emailService = emailService;
        }

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="registerBody"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous]
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
    }
}