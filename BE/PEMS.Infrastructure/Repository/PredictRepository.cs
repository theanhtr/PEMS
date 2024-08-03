using Dapper;
using PEMS.Domain;
using System.Data;

namespace PEMS.Infrastructure
{
    /// <summary>
    /// Triển khai bằng dapper và mysql
    /// </summary>
    /// Created By: TTANH (02/08/2024)
    public class PredictRepository : CodeRepository<Predict>, IPredictRepository
    {
        #region Constructor
        public PredictRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Methods
        #endregion
    }
}
