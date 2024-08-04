using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PEMS.Application;
using PEMS.Domain;
using PEMS.Infrastructure;

namespace PEMS.Controllers
{
    /// <summary>
    /// Controller của layout thông tin cột nhân viên
    /// </summary>
    /// CreatedBy: TTANH (02/08/2024)
    [Route("api/v1/[controller]")]
    [ApiController]
    [RoleAuthorize(3)]
    public class PredictsController : BaseController<Predict, PredictDto, PredictCreateDto, PredictUpdateDto>
    {
        IPredictService _predictService;

        #region Constructors
        public PredictsController(IPredictService predictService) : base(predictService)
        {
            _predictService = predictService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// <returns>Các bản ghi lọc theo các tiêu chí trên</returns>
        /// Created by: TTANH (18/07/2024) 
        [HttpPost("filter")]
        public async Task<IActionResult> FiltersPredictAsync([FromBody] PredictFilterParam predictFilterParam)
        {
            var filterData = await _predictService.FiltersPredictAsync(predictFilterParam);

            return StatusCode(StatusCodes.Status200OK, filterData);
        }
        #endregion
    }
}
