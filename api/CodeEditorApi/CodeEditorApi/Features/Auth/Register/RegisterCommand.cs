using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Services;
using CodeEditorApiDataAccess.StaticData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.Register
{
    public interface IRegisterCommand
    {
        Task<ActionResult> ExecuteAsync(RegisterBody registerModel);
    }

    public class RegisterCommand : IRegisterCommand
    {
        private readonly IRegister _register;
        private readonly IGetUser _getUser;
        private readonly IConfiguration _configuration;
        private readonly IHashService _hashService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;

        public RegisterCommand(IRegister register, 
            IGetUser getUser, 
            IConfiguration configuration, 
            IHashService hashService,
            IEmailService emailService,
            IWebHostEnvironment env)
        {
            _register = register;
            _getUser = getUser;
            _configuration = configuration;
            _hashService = hashService;
            _emailService = emailService;
            _env = env;
        }

        public async Task<ActionResult> ExecuteAsync(RegisterBody registerBody)
        {
            registerBody.Email = registerBody.Email.ToLower();

            var existingUser = await _getUser.ExecuteAsync(registerBody.Email);

            if (existingUser != null) return ApiError.BadRequest("User with email already exists");

            if(registerBody.Role == Roles.Admin || registerBody.Role == Roles.Teacher)
            {
                if (registerBody.AccessCode == null) return ApiError.BadRequest("Invalid Access Code");

                Roles? role = AccessCodeService.CheckAccessCode(registerBody.AccessCode);

                if (!role.HasValue) return ApiError.BadRequest("Invalid Access Code");
                if (role.Value != registerBody.Role) return ApiError.BadRequest("Invalid Access Code");
            }

            registerBody.Password = _hashService.HashPassword(registerBody.Password);

            var newUser = await _register.ExecuteAsync(registerBody);

            var verifyToken = VerifyAccountTokenService.GenerateToken(registerBody.Email);


            var baseURL = "https://siucode.io/auth/verify";

            if(_env.IsDevelopment())
            {
                baseURL = "http://localhost:3000/auth/verify";
            }
            else if(_env.IsStaging())
            {
                baseURL = "https://dev.siucode.io/auth/verify";
            }

            var link = baseURL + "?token=" + verifyToken;

            await _emailService.SendEmailAsync(new MailRequest
            {
                ToEmail = newUser.Email,
                Subject = "[SiuCode] Verify Account",
                Body = $"Click link to verify account: <a href=\"{link}\">here</a>"

            });

            return new OkObjectResult("OK");
        }
    }
}
