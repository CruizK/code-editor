using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Services;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.UpdateUser
{
    public interface IUpdateUserCommand
    {
        public Task<ActionResult<string>> ExecuteAsync(UpdateUserBody updateUserBody);
    }
    public class UpdateUserCommand : IUpdateUserCommand
    {
        private readonly IUpdateUser _updateUser;
        private readonly IGetUser _getUser;
        private readonly IHashService _hashService;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public UpdateUserCommand(IUpdateUser updateUser, IGetUser getUser, IHashService hashService, IJwtService jwtService, IConfiguration configuration)
        {
            _updateUser = updateUser;
            _getUser = getUser;
            _hashService = hashService;
            _jwtService = jwtService;
            _configuration = configuration;
        }
        public async Task<ActionResult<string>> ExecuteAsync(UpdateUserBody updateUserBody)
        {
            var user = await _getUser.GetUserInfo(updateUserBody.Id);
            
            if(user == null)
            {
                return ApiError.BadRequest($"Could not find a User with ID {updateUserBody.Id}");
            }
            if (!updateUserBody.Name.Equals(user.Name) && (_getUser.GetUserByName(updateUserBody.Name) != null))
            {
                return ApiError.BadRequest($"Could not update User's username because another account already exists with username {updateUserBody.Name}");
            }
            if (!updateUserBody.Email.Equals(user.Email) && (_getUser.ExecuteAsync(updateUserBody.Email) != null))
            {
                return ApiError.BadRequest($"Could not update User's email because another account already exists with email {updateUserBody.Email}");
            }
            if(updateUserBody.OldPassword != null)
            {
                if (!_hashService.ComparePassword(user.Hash, updateUserBody.OldPassword))
                {
                    return ApiError.BadRequest($"user input for current password does not match password saved in database");
                }
                if(updateUserBody.NewPassword.Length < 1)
                {
                    return ApiError.BadRequest($"a new password must be provided to update a password.");
                }
            }
            
            updateUserBody.NewPassword = _hashService.HashPassword(updateUserBody.NewPassword);

            var updatedUser = await _updateUser.ExecuteAsync(updateUserBody);

            var token = _jwtService.GenerateToken(_configuration, updatedUser);

            return token;
        }
    }
}
