using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace CodeEditorApi.Features
{
    public static class HttpContextHelper
    {
        public static int retrieveRequestUserId(HttpContext httpContext)
        {
            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
            try
            {
                return int.Parse(userId);
            }
            catch
            {
                throw new System.Exception($"User ID {userId} is invalid");
                //TODO: catch internal error of invalid userId...this should turn into a validation on it's own though. Then call validation in this method.
            }
        }
    }
}
