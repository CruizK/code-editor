using CodeEditorApi.Features.Auth.Login;
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

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterBody registerBody)
        {
            return await _registerCommand.ExecuteAsync(registerBody);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginBody loginBody)
        {
            return await _loginCommand.ExecuteAsync(loginBody);
        }
    }
}