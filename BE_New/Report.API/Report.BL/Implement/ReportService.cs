using Microsoft.AspNetCore.Http;
using Base.BL;
using Report.DL;
using Report.Model;
using Base.Model;
using Microsoft.AspNetCore.Mvc;
using static Dapper.SqlMapper;

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

        #region Methods
        public override async Task BaseServiceMoreProcessInsertAsync(Model.Report report) { 
            report.ReportDate = DateTime.Now;
        }

        public async Task<BaseFilterResponse<Model.Report>> FiltersReportAsync([FromBody] ReportFilterParam reportFilterParam)
        {
            // xử lý param trước khi gửi xuống repository
            DateTime? reportStartDate = reportFilterParam.ReportStartDate?.Date;
            DateTime? reportEndDate = reportFilterParam.ReportEndDate?.Date.AddDays(1).AddSeconds(-1);

            int? pageSize = reportFilterParam.PageSize;
            int? pageNumber = reportFilterParam.PageNumber;

            if (pageSize == null || pageSize <= 0) { pageSize = 999999; }
            if (pageNumber == null || pageNumber < 1) { pageNumber = 1; }



            var reportFilterResult = await _reportRepository.FiltersReportAsync(reportFilterParam, reportStartDate, reportEndDate, pageSize, pageNumber);

            var totalRecord = reportFilterResult.Total;
            var totalPage = Convert.ToInt32(Math.Ceiling((double)totalRecord / (double)pageSize));

            if (pageNumber > totalPage)
            {
                pageNumber = totalPage;
            }


            if (reportFilterResult.Reports == null)
            {
                throw new NotFoundException();
            }

            var currentPage = pageNumber;
            var currentPageRecords = reportFilterResult.Reports.Count();

            var filterData = new BaseFilterResponse<Model.Report>(totalPage, totalRecord, currentPage, currentPageRecords, reportFilterResult.Reports.ToList());

            return filterData;
        }
        #endregion
    }
}
