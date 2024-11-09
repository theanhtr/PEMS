using Base.BL;
using User.Model;

namespace User.BL
{
    /// <summary>
    /// Interface service cho token để controller gọi đến
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IUserService : IBaseService<Model.User>
    {
        Task<string> CreateNewUser(string username, string hashPassword, string fullname);
        Task<Model.User> GetUserInfo(Guid userId);
        Task<Model.User> GetByUserName(string username);
        Task<string> ChangeUserPassWord(ChangePasswordParam changePasswordParam);
    }
}
