using Base.DL;
using Base.Model;
using Dapper;
using System.Data;
using Report.Model;

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
        public async Task<ReportFilterResult> FiltersReportAsync(int? ProvinceId, int? DistrictId, int? WardId, DateTime? ReportStartDate, DateTime? ReportEndDate, string ReportName, int? PageSize, int? PageNumber)
        {
            var procedure = $"Proc_Report_Filter";

            var parameters = new DynamicParameters();
            parameters.Add("@v_ProvinceId", ProvinceId);
            parameters.Add("@v_DistrictId", DistrictId);
            parameters.Add("@v_WardId", WardId);
            parameters.Add("@v_ReportStartDate", ReportStartDate);
            parameters.Add("@v_ReportEndDate", ReportEndDate);
            parameters.Add("@v_ReportName", ReportName);
            parameters.Add("@v_PageSize", PageSize);
            parameters.Add("@v_PageNumber", PageNumber);

            IEnumerable<Model.Report> reports = null;
            TotalResult total = null;

            using (var results = await _unitOfWork.Connection.QueryMultipleAsync(procedure, parameters, commandType: CommandType.StoredProcedure))
            {
                reports = await results.ReadAsync<Model.Report>();
                total = await results.ReadSingleAsync<TotalResult>();
            }

            ReportFilterResult reportFilterResult = new ReportFilterResult
            {
                Reports = reports,
                Total = total.Total
            };

            return reportFilterResult;
        }
        #endregion
    }
}
