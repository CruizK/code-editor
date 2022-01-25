using CodeEditorApiDataAccess.Data;
using CodeEditorApiDataAccess.StaticData;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.Register
{

    public interface IRegister
    {
        Task<User> ExecuteAsync(RegisterBody registerData, int role);
    }

    public class Register : IRegister
    {
        private readonly CodeEditorApiContext _context;

        public Register(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<User> ExecuteAsync(RegisterBody registerData, int role)
        {
            var user = new User
            {
                Email = registerData.Email,
                Name = registerData.Name,
                Hash = registerData.Password,
                RoleId = role
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
