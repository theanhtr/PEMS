using Microsoft.AspNetCore.Http;
using Base.BL;
using Report.DL;

namespace Report.BL
{
    public class ReportService : BaseService<Model.Report>, IReportService
    {
        IReportRepository _reportRepository;
        #region Constructor
        public ReportService(IReportRepository reportRepository, IHttpContextAccessor httpContextAccessor) : base(reportRepository, httpContextAccessor)
        {
            _reportRepository = reportRepository;
        }
        #endregion
    }
}
