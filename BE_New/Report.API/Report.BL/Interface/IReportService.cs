using Base.BL;
using Base.Model;
using Microsoft.AspNetCore.Mvc;
using Report.Model;

namespace Report.BL
{
    /// <summary>
    /// Interface service cho token để controller gọi đến
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IReportService : IBaseService<Model.Report>
    {
        Task<BaseFilterResponse<Model.Report>> FiltersReportAsync(ReportFilterParam reportFilterParam);
    }
}
