using CodeEditorApiDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.Login
{
    public interface ILogin
    {
        Task<User> ExecuteAsync(string email);
    }
    public class Login : ILogin
    {
        private readonly CodeEditorApiContext _context;
        public Login(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<User> ExecuteAsync(string email)
        {
            var user = await _context.Users
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return user;
        }
    }
}
