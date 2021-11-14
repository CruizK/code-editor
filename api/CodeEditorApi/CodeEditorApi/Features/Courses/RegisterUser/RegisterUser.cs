using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApi.Features.Tutorials.RegisterUser;
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
        private readonly IRegisterUserTutorialCommand _registerUserTutorialCommand;
        private readonly IGetCourseTutorialsCommand _getCourseTutorialsCommand;

        public RegisterUser(CodeEditorApiContext context, IRegisterUserTutorialCommand registerUserTutorialCommand, IGetCourseTutorialsCommand getCourseTutorialsCommand)
        {
            _context = context;
            _registerUserTutorialCommand = registerUserTutorialCommand;
            _getCourseTutorialsCommand = getCourseTutorialsCommand;
        }

        public async Task<UserRegisteredCourse> ExecuteAsync(int userId, UserRegisteredCourse userRegisteredCourse)
        {

            await _context.UserRegisteredCourses.AddAsync(userRegisteredCourse);

            var courseTutorials = await _getCourseTutorialsCommand.ExecuteAsync(userRegisteredCourse.CourseId);
            await _registerUserTutorialCommand.ExecuteAsync(userId, courseTutorials.Value);

            await _context.SaveChangesAsync();
            return userRegisteredCourse;
        }
    }
}
