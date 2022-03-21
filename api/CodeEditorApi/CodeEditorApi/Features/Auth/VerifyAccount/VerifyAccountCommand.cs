using CodeEditorApi.Errors;
using CodeEditorApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.VerifyAccount
{
    public interface IVerifyAccountCommand
    {
        Task<ActionResult<string>> ExecuteAsync(VerifyAccountBody body);
    }

    public class VerifyAccountCommand : IVerifyAccountCommand
    {
        private readonly IVerifyAccount _veritfyAccount;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public VerifyAccountCommand(
            IVerifyAccount verifyAccount,
            IJwtService jwtService,
            IConfiguration configuration
            )
        {
            _veritfyAccount = verifyAccount;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<ActionResult<string>> ExecuteAsync(VerifyAccountBody body)
        {
            string email = VerifyAccountTokenService.CheckToken(body.VerifyToken);

            if(email == null)
            {
                return ApiError.BadRequest("Invalid Token");
            }


            var user = await _veritfyAccount.VerifyUser(email);

            if(user == null)
            {
                return ApiError.BadRequest("User with email does not exist");
            }

            var token = _jwtService.GenerateToken(_configuration, user);

            return token;
        }
    }
}
