using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PEMS.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PEMS.Application
{
    public class AuthService : IAuthService
    {
        ITokenService _tokenService;
        IUserService _userService;

        #region Constructor
        public AuthService(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }
        #endregion

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<object> Login(string username, string password)
        {
            // Validate user credentials (use your own validation logic)
            var user = await _userService.GetByUserName(username);

            if (user == null)
            {
                throw new ValidateException(StatusErrorCode.NotFoundData, "Không tìm thấy tài khoản", "");
            }

            if (VerifyPassword(password, user.Password))
            {
                var token = _tokenService.GenerateToken(user.UserId.ToString(), user.Fullname);
                return token;
            }
            else
            {
                throw new ValidateException(StatusErrorCode.WrongPassword, "Sai mật khẩu", "");
            }
        }
    }
}
