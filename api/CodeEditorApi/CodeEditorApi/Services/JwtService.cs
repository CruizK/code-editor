using CodeEditorApiDataAccess.Data;
using CodeEditorApiDataAccess.StaticData;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeEditorApi.Services
{

    public interface IJwtService
    {
        string GenerateToken(IConfiguration configuration, User user);
    }

    public class JwtService : IJwtService
    {
        public string GenerateToken(IConfiguration configuration, User user)
        {

            var secret = configuration["Jwt:Key"];
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

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
