using Microsoft.AspNetCore.Mvc;
using Base.BL;

namespace Authen.BL
{
    /// <summary>
    /// Interface service cho token để controller gọi đến
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IAuthService
    {
        Task<object> Login(string username, string password);

        Task<string> Register(string username, string password, string fullname);
    }
}
