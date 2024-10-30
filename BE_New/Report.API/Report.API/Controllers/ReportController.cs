using Microsoft.AspNetCore.Mvc;
using Report.BL;
using Report.Model;
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
    //[RoleAuthorize(3)]
    public class ReportController : BaseController<Model.Report>
    {
        IReportService _reportService;
        
        public ReportController(IReportService reportService) : base(reportService)
        {
            _reportService = reportService;
        }

        #region Methods
        /// <summary>
        /// <returns>Các bản ghi lọc theo các tiêu chí trên</returns>
        /// Created by: TTANH (18/07/2024) 
        [HttpPost("filter")]
        public async Task<IActionResult> FiltersReportAsync([FromBody] ReportFilterParam reportFilterParam)
        {
            var filterData = await _reportService.FiltersReportAsync(reportFilterParam);

            return StatusCode(StatusCodes.Status200OK, filterData);
        }
        #endregion
    }
}
