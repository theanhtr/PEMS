using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Interface service cho token để controller gọi đến
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface ITokenService
    {
       object GenerateToken(string userId, string fullname);
    }
}
