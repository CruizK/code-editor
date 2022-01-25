using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.Register
{
    public interface IRegisterCommand
    {
        Task<ActionResult<string>> ExecuteAsync(RegisterBody registerModel, CodeEditorApiDataAccess.StaticData.Roles role);
    }

    public class RegisterCommand : IRegisterCommand
    {
        private readonly IRegister _register;
        private readonly IGetUser _getUser;
        private readonly IConfiguration _configuration;
        private readonly IHashService _hashService;
        private readonly IJwtService _jwtService;

        public RegisterCommand(IRegister register, 
            IGetUser getUser, 
            IConfiguration configuration, 
            IHashService hashService,
            IJwtService jwtService)
        {
            _register = register;
            _getUser = getUser;
            _configuration = configuration;
            _hashService = hashService;
            _jwtService = jwtService;
        }

        public async Task<ActionResult<string>> ExecuteAsync(RegisterBody registerBody, CodeEditorApiDataAccess.StaticData.Roles role)
        {
            var existingUser = await _getUser.ExecuteAsync(registerBody.Email);

            if (existingUser != null) return ApiError.BadRequest("User with email already exists");

            registerBody.Password = _hashService.HashPassword(registerBody.Password);

            var newUser = await _register.ExecuteAsync(registerBody, (int)role);

            var token = _jwtService.GenerateToken(_configuration, newUser);

            return token;
        }
    }
}
