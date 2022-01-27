using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApiDataAccess.StaticData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.GetAccess
{
    public interface IGetAccessCommand
    {
        public Task<ActionResult<Guid>> ExecuteAsync(int userId);
    }
    public class GetAccessCommand : IGetAccessCommand
    {
        private readonly IGetAccess _getAccess;
        private readonly IGetUser _getUser;
        public GetAccessCommand(IGetAccess getAccess, IGetUser getUser)
        {
            _getAccess = getAccess;
            _getUser = getUser;
        }

        public async Task<ActionResult<Guid>> ExecuteAsync(int userId)
        {
            if(_getUser.GetUserInfo(userId).Result.RoleId == (int)Roles.Admin)
            {
                return await _getAccess.ExecuteAsync();
            }

            return ApiError.BadRequest($"User {userId} is not an Admin. Only Admin are allowed to generate access codes.");
            
        }
    }
}
