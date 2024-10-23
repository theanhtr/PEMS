using Base.DL;

namespace Report.DL
{
    /// <summary>
    /// Triển khai bằng dapper và mysql
    /// </summary>
    /// Created By: TTANH (02/08/2024)
    public class ReportRepository : BaseRepository<Model.Report>, IReportRepository
    {
        #region Constructor
        public ReportRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Methods
        #endregion
    }
}
