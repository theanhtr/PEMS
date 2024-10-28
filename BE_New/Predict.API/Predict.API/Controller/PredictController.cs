using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Base.Model;
using Predict.BL;
using Predict.Model;
using Base.Controller;

namespace Predict.API
{
    /// <summary>
    /// Controller của layout thông tin cột nhân viên
    /// </summary>
    /// CreatedBy: TTANH (02/08/2024)
    [Route("api/v1/[controller]")]
    [ApiController]
    [RoleAuthorize(3)]
    public class PredictController : BaseController<Model.Predict>
    {
        IPredictService _predictService;

        #region Constructors
        public PredictController(IPredictService predictService) : base(predictService)
        {
            _predictService = predictService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// <returns>Các bản ghi lọc theo các tiêu chí trên</returns>
        /// Created by: TTANH (18/07/2024) 
        [HttpGet("season-end")]
        public async Task<IActionResult> EndSeasonAsync([FromQuery] Guid? PredictId)
        {
            if (PredictId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id không được để trống");
            }

            var seasonEnd = await _predictService.EndSeasonAsync(PredictId);

            return StatusCode(StatusCodes.Status200OK, seasonEnd);
        }

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
