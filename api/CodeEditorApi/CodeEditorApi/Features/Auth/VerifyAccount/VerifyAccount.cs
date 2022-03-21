using CodeEditorApiDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.VerifyAccount
{
    public interface IVerifyAccount
    {
        Task<User> VerifyUser(string email);
    }

    public class VerifyAccount : IVerifyAccount
    {
        private readonly CodeEditorApiContext _context;

        public VerifyAccount(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<User> VerifyUser(string email)
        {
            var user = await _context.Users.Where(x => x.Email == email).SingleAsync();

            if (user != null) user.IsConfirmed = true;

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
