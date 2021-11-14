using CodeEditorApiDataAccess.Data;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.RegisterUser
{
    public interface IRegisterUser
    {
        public Task<UserRegisteredCourse> ExecuteAsync(int userId, UserRegisteredCourse userRegisteredCourse);
    }
    public class RegisterUser : IRegisterUser
    {
        private readonly CodeEditorApiContext _context;

        public RegisterUser(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<UserRegisteredCourse> ExecuteAsync(int userId, UserRegisteredCourse userRegisteredCourse)
        {

            await _context.UserRegisteredCourses.AddAsync(userRegisteredCourse);
            await _context.SaveChangesAsync();
            return userRegisteredCourse;
        }
    }
}
