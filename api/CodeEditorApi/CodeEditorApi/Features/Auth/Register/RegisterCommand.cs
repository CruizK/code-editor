using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.Register
{
    public interface IRegisterCommand
    {
        Task<ActionResult<string>> ExecuteAsync(RegisterBody registerModel);
    }

    public class RegisterCommand : IRegisterCommand
    {
        private readonly IRegister _register;
        private readonly IGetUser _getUser;
        private readonly IConfiguration _configuration;

        public RegisterCommand(IRegister register, IGetUser getUser, IConfiguration configuration)
        {
            _register = register;
            _getUser = getUser;
            _configuration = configuration;
        }

        public async Task<ActionResult<string>> ExecuteAsync(RegisterBody registerBody)
        {
            var existingUser = await _getUser.ExecuteAsync(registerBody.Email);

            if (existingUser != null) return ApiError.BadRequest("User with email already exists");

            registerBody.Password = HashHelper.HashPassword(registerBody.Password);

            var newUser = await _register.ExecuteAsync(registerBody);

            var token = JwtHelper.GenerateToken(_configuration, newUser);

            return token;
        }
    }
}
