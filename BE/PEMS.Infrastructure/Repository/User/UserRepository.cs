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
        #endregion
    }
}
