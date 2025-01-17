﻿namespace PEMS.Domain
{
    /// <summary>
    /// Interface để tương tác với DB User
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IUserRepository : ICodeRepository<User>
    {
        User GetByUserName(string username);
        User? GetByUserId(Guid userId);
        Task<string> CreateNewUser(string username, string hashPassword, string fullname);
    }
}
