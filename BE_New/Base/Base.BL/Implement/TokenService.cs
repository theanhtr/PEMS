using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Base.BL
{
    /// <summary>
    /// Class triển khai token service được gọi từ controller
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public class TokenService : ITokenService
    {
        #region Constructor
        public TokenService(IConfiguration configuration)
        {
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }
        #endregion

        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public object GenerateToken(string userId, string fullname)
        {
            var expirationTime = DateTime.Now.AddDays(1);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", userId),
            new Claim("Fullname", fullname),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenData = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: expirationTime,
                signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

            return new
            {
                expirationTime,
                token
            };
        }
    }
}
