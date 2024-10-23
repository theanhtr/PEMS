using Microsoft.AspNetCore.Mvc;
using Report.BL;
using Base.Model;
using Base.Controller;

namespace Report.API
{
    /// <summary>
    /// Controller của phòng ban
    /// </summary>
    /// CreatedBy: TTANH (12/07/2024)
    [Route("api/v1/[controller]")]
    [ApiController]
    [RoleAuthorize(3)]
    public class ReportController : BaseController<Model.Report>
    {
        IReportService _reportService;
        
        public ReportController(IReportService reportService) : base(reportService)
        {
            _reportService = reportService;
        }
    }
}
