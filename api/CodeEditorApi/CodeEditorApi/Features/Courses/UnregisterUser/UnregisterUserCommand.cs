using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Courses.UnregisterUser
{
    public interface IUnregisterUserCommand
    {
        public Task<ActionResult<UserRegisteredCourse>> ExecuteAsync(int userId, UnregisterUserBody unregisterUserBody);
    }

    public class UnregisterUserCommand : IUnregisterUserCommand
    {

        private readonly IUnregisterUser _unregisterUser;

        public UnregisterUserCommand(IUnregisterUser unregisterUser)
        {
            _unregisterUser = unregisterUser;
        }

        public async Task<ActionResult<UserRegisteredCourse>> ExecuteAsync(int userId, UnregisterUserBody unregisterUserBody)
        {
            return await _unregisterUser.ExecuteAsync(userId, unregisterUserBody.CourseId);
        }
    }
}
