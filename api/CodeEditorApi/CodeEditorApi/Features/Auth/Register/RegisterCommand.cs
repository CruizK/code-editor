using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Services;
using CodeEditorApiDataAccess.StaticData;
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

        public async Task<ActionResult<string>> ExecuteAsync(RegisterBody registerBody)
        {
            var existingUser = await _getUser.ExecuteAsync(registerBody.Email);

            if (existingUser != null) return ApiError.BadRequest("User with email already exists");

            if(registerBody.Role == Roles.Admin || registerBody.Role == Roles.Teacher)
            {
                if (registerBody.AccessCode == "") return ApiError.BadRequest("Invalid Access Code");

                Roles? role = AccessCodeService.CheckAccessCode(registerBody.AccessCode);

                if (!role.HasValue) return ApiError.BadRequest("Invalid Access Code");
                if (role.Value != registerBody.Role) return ApiError.BadRequest("Invalid Access Code");
            }

            registerBody.Password = _hashService.HashPassword(registerBody.Password);

            var newUser = await _register.ExecuteAsync(registerBody, registerBody.Role);

            var token = _jwtService.GenerateToken(_configuration, newUser);

            return token;
        }
    }
}
