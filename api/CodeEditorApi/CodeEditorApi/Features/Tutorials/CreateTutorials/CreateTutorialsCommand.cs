using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Features.Tutorials.CreateUserTutorials;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiDataAccess.StaticData;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Tutorials.CreateTutorials
{
    public interface ICreateTutorialsCommand
    {
        public Task<ActionResult<Tutorial>> ExecuteAsync(int userId, CreateTutorialsBody createTutorialsBody);
    }
    public class CreateTutorialsCommand : ICreateTutorialsCommand
    {
        private readonly ICreateTutorials _createTutorials;
        private readonly IGetTutorials _getTutorials;
        private readonly IGetUser _getUser;
        private readonly ICreateUserTutorials _createUserTutorials;

        public CreateTutorialsCommand(ICreateTutorials createTutorials, IGetTutorials getTutorials, IGetUser getUser, ICreateUserTutorials createUserTutorials)
        {
            _createTutorials = createTutorials;
            _getTutorials = getTutorials;
            _getUser = getUser;
            _createUserTutorials = createUserTutorials;
        }

        public async Task<ActionResult<Tutorial>> ExecuteAsync(int userId, CreateTutorialsBody createTutorialsBody)
        {
            var tutorialsInCourse = await _getTutorials.GetCourseTutorials(createTutorialsBody.CourseId);
            var user = _getUser.GetUserInfo(userId);
            if(user.Result.RoleId != (int)Roles.Teacher)
            {
                return ApiError.BadRequest("User is not authorized to create tutorials.");
            }
            if (tutorialsInCourse.Find(t => t.Title == createTutorialsBody.Title) != null)
            {
                return ApiError.BadRequest($"A tutorial already exists under course {createTutorialsBody.CourseId} with the same title '{createTutorialsBody.Title}'");
            }
            var tutorial = await _createTutorials.ExecuteAsync(userId, createTutorialsBody);

            if(tutorial != null)
            {
                var users = await _getUser.GetAllUsersRegisteredToCourse(createTutorialsBody.CourseId);
                if (users != null)
                {
                    var addCount = await _createUserTutorials.ExecuteAsync(tutorial.Id, users);
                    if (addCount != users.Count)
                    {
                        return ApiError.BadRequest($"Failed to register Students under Course {createTutorialsBody.CourseId} to new Tutorial");
                    }
                }
                    
            }

            return tutorial;           
        }
    }
}
