using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.Login
{
    public interface ILoginCommand
    {
        Task<ActionResult<string>> ExecuteAsync(LoginBody loginBody);
    }

    public class LoginCommand : ILoginCommand
    {
        private readonly IGetUser _getUser;
        private readonly IConfiguration _configuration;
        public LoginCommand(IGetUser getUser, IConfiguration configuration)
        {
            _getUser = getUser;
            _configuration = configuration;
        }

        public async Task<ActionResult<string>> ExecuteAsync(LoginBody loginBody)
        {
            var user = await _getUser.ExecuteAsync(loginBody.Email);

            if (user == null) return ApiError.BadRequest("User does not exist");

            if(HashHelper.ComparePassword(user.Hash, loginBody.Password))
            {
                return JwtHelper.GenerateToken(_configuration, user);
            }

            return ApiError.BadRequest("Password was incorrect");
        }
    }
}
