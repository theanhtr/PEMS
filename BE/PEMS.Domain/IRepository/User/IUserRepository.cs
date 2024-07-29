﻿namespace PEMS.Domain
{
    /// <summary>
    /// Interface để tương tác với DB User
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByUserName(string username);
        User? GetByUserId(Guid userId);
    }
}