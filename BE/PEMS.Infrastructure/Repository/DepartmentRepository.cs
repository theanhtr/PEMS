using PEMS.Domain;

namespace PEMS.Infrastructure
{
    /// <summary>
    /// Triển khai bằng dapper và mysql
    /// </summary>
    /// Created By: TTANH (12/07/2023)
    public class DepartmentRepository : CodeRepository<Department>, IDepartmentRepository
    {
        #region Constructor
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion
    }
}
