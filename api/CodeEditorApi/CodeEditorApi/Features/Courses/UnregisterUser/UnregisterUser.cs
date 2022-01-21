using CodeEditorApi.Features.Tutorials.GetTutorials;
using UnregisterUserTutorial = CodeEditorApi.Features.Tutorials.UnregisterUser;
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
        private readonly IGetCourseTutorialsCommand _getCourseTutorialsCommand;
        private readonly UnregisterUserTutorial.IUnregisterUserCommand _unregisterUserCommand;

        public UnregisterUser(CodeEditorApiContext context, IGetCourseTutorialsCommand getCourseTutorialsCommand,
            UnregisterUserTutorial.IUnregisterUserCommand unregisterUserCommand)
        {
            _context = context;
            _getCourseTutorialsCommand = getCourseTutorialsCommand;
            _unregisterUserCommand = unregisterUserCommand;
        }

        public async Task<UserRegisteredCourse> ExecuteAsync(int userId, int courseId)
        {
            var userRegisteredCourse = await _context.UserRegisteredCourses.FindAsync(courseId, userId);

            if(userRegisteredCourse != null)
            {
                _context.UserRegisteredCourses.Remove(userRegisteredCourse);
                await _context.SaveChangesAsync();

                var courseTutorials = await _getCourseTutorialsCommand.ExecuteAsync(courseId);

                if (courseTutorials != null)
                {
                    await _unregisterUserCommand.ExecuteAsync(courseId, userId);
                }
                                
            }

            return userRegisteredCourse;
        }
    }
}
