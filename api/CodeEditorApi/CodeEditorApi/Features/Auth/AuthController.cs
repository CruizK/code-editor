using CodeEditorApi.Features.Auth.Login;
using CodeEditorApi.Features.Auth.Register;
using CodeEditorApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public AuthController(IRegisterCommand registerCommand, ILoginCommand loginCommand, IConfiguration configuration)
        {
            _registerCommand = registerCommand;
            _loginCommand = loginCommand;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task Register([FromBody] RegisterBody registerBody)
        {
            await _registerCommand.ExecuteAsync(registerBody);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginBody loginBody)
        {
            var user = await _loginCommand.ExecuteAsync(loginBody);

            if(user != null)
            {
                var token = JwtHelper.GenerateToken(
                    _configuration["Jwt:Key"],
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    user);
                return token;
            }

            return Unauthorized();
        }
    }
}