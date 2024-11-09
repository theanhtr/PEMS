using Base.DL;

namespace User.DL
{
    /// <summary>
    /// Interface để tương tác với DB User
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IUserRepository : IBaseRepository<Model.User>
    {
        Model.User GetByUserName(string username);
        Model.User? GetByUserId(Guid userId);
        string GetUserPassword(Guid userId);
        bool ChangeUserPassword(Guid userId, string password);
        Task<string> CreateNewUser(string username, string hashPassword, string fullname);
    }
}
