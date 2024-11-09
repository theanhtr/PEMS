using Microsoft.AspNetCore.Http;
using Base.BL;
using User.DL;
using User.Model;
using Base.Model;

namespace User.BL
{
    public class UserService : BaseService<Model.User>, IUserService
    {
        IUserRepository _userRepository;
        #region Constructor
        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : base(userRepository, httpContextAccessor)
        {
            _userRepository = userRepository;
        }
        #endregion

        public async Task<string> CreateNewUser(string username, string hashPassword, string fullname)
        {
            var user = await _userRepository.CreateNewUser(username, hashPassword, fullname);

            return user;
        }

        public async Task<Model.User> GetUserInfo(Guid userId)
        {
            var user = _userRepository.GetByUserId(userId);

            return user;
        }

        public async Task<Model.User> GetByUserName(string username)
        {
            return _userRepository.GetByUserName(username);
        }

        public async Task<Model.User> GetRoleID(Guid userId)
        {
            return _userRepository.GetByUserId(userId);
        }

        public async Task<string> GetUserPassword(Guid userId)
        {
            return _userRepository.GetUserPassword(userId);
        }

        public async Task<string> ChangeUserPassWord(ChangePasswordParam changePasswordParam)
        {
            var oldPassword = await GetUserPassword(Guid.Parse(changePasswordParam.UserId));

            if (!HelperApplication.VerifyPassword(changePasswordParam.OldPassword, oldPassword))
            {
                throw new ValidateException(StatusErrorCode.WrongPassword, "Sai mật khẩu cũ", "");
            }

            var newPassword = HelperApplication.HashPassword(changePasswordParam.Password);

            _userRepository.ChangeUserPassword(Guid.Parse(changePasswordParam.UserId), newPassword);

            return "success";
        }

        public override Task BaseServiceMoreProcessInsertAsync(Model.User entityCreateDto)
        {
            entityCreateDto.Password = BCrypt.Net.BCrypt.HashPassword("12345678@Abc");
            return base.BaseServiceMoreProcessInsertAsync(entityCreateDto);
        }
    }
}
