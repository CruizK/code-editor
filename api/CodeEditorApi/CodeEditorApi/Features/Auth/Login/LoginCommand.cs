using CodeEditorApi.Helpers;
using CodeEditorApiDataAccess.Data;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.Login
{
    public interface ILoginCommand
    {
        Task<User> ExecuteAsync(LoginBody loginBody);
    }

    public class LoginCommand : ILoginCommand
    {
        private readonly ILogin _login;
        public LoginCommand(ILogin login)
        {
            _login = login;
        }

        public async Task<User> ExecuteAsync(LoginBody loginBody)
        {
            var user = await _login.ExecuteAsync(loginBody.Email);

            if (user == null) return null;

            if(HashHelper.ComparePassword(user.Hash, loginBody.Password))
            {
                return user;
            }

            return null;
        }
    }
}
