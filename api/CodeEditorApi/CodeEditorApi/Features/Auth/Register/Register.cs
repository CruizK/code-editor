using CodeEditorApiDataAccess.Data;
using CodeEditorApiDataAccess.StaticData;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.Register
{

    public interface IRegister
    {
        Task ExecuteAsync(RegisterBody registerData);
    }

    public class Register : IRegister
    {
        private readonly CodeEditorApiContext _context;

        public Register(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(RegisterBody registerData)
        {
            var user = new User
            {
                Email = registerData.Email,
                Name = registerData.Name,
                Hash = registerData.Password,
                RoleId = (int)Roles.Student
            };

            await _context.Users.AddAsync(user).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
