using Dapper;
using PEMS.Domain;
using System.Data;

namespace PEMS.Infrastructure
{
    /// <summary>
    /// Triển khai bằng dapper và mysql
    /// </summary>
    /// Created By: TTANH (02/08/2024)
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        #region Constructor
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Methods
        public User GetByUserName(string username)
        {
            var sql = "SELECT * FROM user WHERE Username = @UserName";
            var user = _unitOfWork.Connection.QueryFirstOrDefault<User>(sql, new { UserName = username }, commandType: CommandType.Text);
            return user;
        }

        public User? GetByUserId(Guid userId)
        {
            var sql = "SELECT * FROM user WHERE UserId = @userId";
            var user = _unitOfWork.Connection.QueryFirstOrDefault<User>(sql, new { userId = userId }, commandType: CommandType.Text);
            return user;
        }

        public async Task<string> CreateNewUser(string username, string hashPassword, string fullname)
        {
            var sql = "INSERT INTO user (Username, Password, FullName, RoleID, UserId) VALUES (@Username, @Password, @FullName, 3, @UserId)";
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, new { Username = username, Password = hashPassword, FullName = fullname, UserId = Guid.NewGuid().ToString() }, commandType: CommandType.Text);
            return result > 0 ? "Success" : "Failed";
        }
        #endregion
    }
}
