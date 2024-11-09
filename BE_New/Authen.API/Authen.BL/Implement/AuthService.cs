using Base.BL;
using Base.Model;
using Authen.DL;

namespace Authen.BL
{
    public class AuthService : IAuthService
    {
        ITokenService _tokenService;
        IAuthRepository _authRepository;

        #region Constructor
        public AuthService(ITokenService tokenService, IAuthRepository authRepository)
        {
            _tokenService = tokenService;
            _authRepository = authRepository;
        }
        #endregion

        #region User Service
        public async Task<string> CreateNewUser(string username, string hashPassword, string fullname)
        {
            var user = await _authRepository.CreateNewUser(username, hashPassword, fullname);

            return user;
        }

        public async Task<Model.User> GetUserInfo(Guid userId)
        {
            var user = _authRepository.GetByUserId(userId);

            return user;
        }

        public async Task<Model.User> GetByUserName(string username)
        {
            return _authRepository.GetByUserName(username);
        }

        public async Task<Model.User> GetRoleID(Guid userId)
        {
            return _authRepository.GetByUserId(userId);
        }
        #endregion

        public async Task<object> Login(string username, string password)
        {
            // Validate user credentials (use your own validation logic)
            var user = await GetByUserName(username);

            if (user == null)
            {
                throw new ValidateException(StatusErrorCode.NotFoundData, "Không tìm thấy tài khoản", "");
            }

            if (HelperApplication.VerifyPassword(password, user.Password))
            {
                var token = _tokenService.GenerateToken(user.UserId.ToString(), user.Fullname);
                return new
                {
                    token,
                    user.RoleID
                };
            }
            else
            {
                throw new ValidateException(StatusErrorCode.WrongPassword, "Sai mật khẩu", "");
            }
        }

        public async Task<string> Register(string username, string password, string fullname)
        {
            var userExists = await GetByUserName(username);

            if (userExists != null)
            {
                throw new ValidateException(StatusErrorCode.CodeDuplicate, "Tài khoản đã tồn tại", "");
            }

            // Validate user credentials (use your own validation logic)
            var user = await CreateNewUser(username, HelperApplication.HashPassword(password), fullname);

            return user;
        }
    }
}
