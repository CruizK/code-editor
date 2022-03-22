using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Services;
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
        private readonly IHashService _hashService;
        private readonly IJwtService _jwtService;
        public LoginCommand(IGetUser getUser, 
            IConfiguration configuration, 
            IHashService hashService,
            IJwtService jwtService)
        {
            _getUser = getUser;
            _configuration = configuration;
            _hashService = hashService;
            _jwtService = jwtService;
        }

        public async Task<ActionResult<string>> ExecuteAsync(LoginBody loginBody)
        {
            loginBody.Email = loginBody.Email.ToLower();

            var user = await _getUser.ExecuteAsync(loginBody.Email);

            if (user == null || !user.IsConfirmed) return ApiError.BadRequest("User does not exist or not verified");


            if (_hashService.ComparePassword(user.Hash, loginBody.Password))
            {
                return _jwtService.GenerateToken(_configuration, user);
            }

            return ApiError.BadRequest("Password was incorrect");
        }
    }
}
