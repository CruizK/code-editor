using CodeEditorApiDataAccess.Data;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.UnregisterUser
{
    public interface IUnregisterUser
    {
        public Task<UserRegisteredCourse> ExecuteAsync(int userId, int courseId);
    }
    public class UnregisterUser : IUnregisterUser
    {
        private readonly CodeEditorApiContext _context;

        public UnregisterUser(CodeEditorApiContext context)
        {
            _context = context;
        }

        public async Task<UserRegisteredCourse> ExecuteAsync(int userId, int courseId)
        {
            var userRegisteredCourse = await _context.UserRegisteredCourses.FindAsync(userId, courseId);

            if(userRegisteredCourse != null)
            {
                _context.UserRegisteredCourses.Remove(userRegisteredCourse);
                await _context.SaveChangesAsync();
            }

            return userRegisteredCourse;
        }
    }
}
