using Base.DL;
using Report.Model;

namespace Report.DL
{
    /// <summary>
    /// Interface để tương tác với DB User
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IReportRepository : IBaseRepository<Model.Report>
    {
        Task<ReportFilterResult> FiltersReportAsync(int? ProvinceId, int? DistrictId, int? WardId, DateTime? ReportStartDate, DateTime? ReportEndDate, string ReportName, int? PageSize, int? PageNumber);
    }
}
