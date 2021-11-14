using CodeEditorApi.Errors;
using CodeEditorApi.Features.Courses.GetCourses;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.RegisterUser
{
    public interface IRegisterUserCommand
    {
        public Task<ActionResult<UserRegisteredCourse>> ExecuteAsync(int userId, RegisterUserBody registerUserBody);
    }

    public class RegisterUserCommand : IRegisterUserCommand
    {

        private readonly IRegisterUser _registerUser;
        private readonly IGetCourses _getCourses;

        public RegisterUserCommand(IRegisterUser registerUser, IGetCourses getCourses)
        {
            _registerUser = registerUser;
            _getCourses = getCourses;
        }

        public async Task<ActionResult<UserRegisteredCourse>> ExecuteAsync(int userId, RegisterUserBody registerUserBody)
        {
            var userRegisteredCourse = new UserRegisteredCourse
            {
                UserId = userId,
                CourseId = registerUserBody.CourseId
            };

            var courses = await _getCourses.GetUserCourses(userId);

            if(courses.Select(x => x.Id).Contains(registerUserBody.CourseId))
            {
                return ApiError.BadRequest("User already registered to course");
            }

            return await _registerUser.ExecuteAsync(userId, userRegisteredCourse);
        }
    }
}
