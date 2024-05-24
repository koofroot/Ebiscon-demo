using EbisconDemo.Data.Models;
using EbisconDemo.Services.Constants;
using EbisconDemo.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EbisconDemo.Services.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public Task<string> GenerateTokenAsync(User user, DateTime tokenExpirationTime)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EbisconConstants.JwtSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.UserType.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: EbisconConstants.JwtIssuer,
                audience: EbisconConstants.JwtAudience,
                claims: claims,
                expires: tokenExpirationTime,
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
