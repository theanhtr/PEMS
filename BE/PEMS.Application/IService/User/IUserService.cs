using Microsoft.AspNetCore.Mvc;
using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Interface service cho token để controller gọi đến
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IUserService
    {
        Task<string> CreateNewUser(string username, string hashPassword, string fullname);
        Task<UserDto> GetUserInfo(Guid userId);
        Task<User> GetByUserName(string username);
    }
}
