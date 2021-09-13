using CodeEditorApi.Helpers;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.Register
{
    public interface IRegisterCommand
    {
        Task ExecuteAsync(RegisterBody registerModel);
    }

    public class RegisterCommand : IRegisterCommand
    {
        public IRegister _register;

        public RegisterCommand(IRegister register)
        {
            _register = register;
        }

        public async Task ExecuteAsync(RegisterBody registerBody)
        {
            // Hash the password
            registerBody.Password = HashHelper.HashPassword(registerBody.Password);

            await _register.ExecuteAsync(registerBody);
        }
    }
}
