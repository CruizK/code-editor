using CodeEditorApi.Features.Auth.Register;
using CodeEditorApiDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeEditorApi.Features.Auth.UpdateUser
{
    public interface IUpdateUser
    {
        public Task<User> ExecuteAsync(UpdateUserBody updateUserBody);
    }
    public class UpdateUser : IUpdateUser
    {
        private readonly CodeEditorApiContext _context;

        public UpdateUser(CodeEditorApiContext context)
        {
            _context = context;
        }
        public async Task<User> ExecuteAsync(UpdateUserBody updateUserBody)
        {
            var user = await _context.Users.Where(u => u.Id == updateUserBody.Id).FirstOrDefaultAsync();

            if(user != null)
            {
                user.Email = updateUserBody.Email;
                if(updateUserBody.NewPassword != null)
                {
                    user.Hash = updateUserBody.NewPassword;
                }                
                await _context.SaveChangesAsync();
            }

            return user;
        }
    }
}
