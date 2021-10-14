using CodeEditorApiDataAccess.Data;
using CodeEditorApiDataAccess.StaticData;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeEditorApi.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateToken(string secret, string issuer, string audience, User user)
        {
            
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(Roles), user.RoleId))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddDays(2),
                issuer: issuer,
                audience: audience,
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
