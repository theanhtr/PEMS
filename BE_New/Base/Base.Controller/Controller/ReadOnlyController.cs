using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Base.BL;
using Base.Model;

namespace Base.Controller
{
    /// <summary>
    /// Controller base gồm đọc và lọc với đối tượng không có code
    /// </summary>
    /// CreatedBy: TTANH (18/07/2024)
    [ApiController]
    public class ReadOnlyController<TEntity> : ControllerBase
    {
        #region Fields
        private readonly IReadOnlyService<TEntity> _readOnlyService;
        #endregion

        #region Constructor
        public ReadOnlyController(IReadOnlyService<TEntity> readOnlyService)
        {
            _readOnlyService = readOnlyService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm lấy dữ liệu bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// CreatedBy: TTANH (18/07/2024)
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            await ValidateBeforeActionBase(BaseAction.Get);
            var entities = await _readOnlyService.GetAsync();

            return StatusCode(StatusCodes.Status200OK, entities);
        }

        /// <summary>
        /// Hàm lấy dữ liệu bản ghi theo id
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: TTANH (18/07/2024)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            await ValidateBeforeActionBase(BaseAction.Get);
            var entity = await _readOnlyService.GetByIdAsync(id);

            return StatusCode(StatusCodes.Status200OK, entity);
        }

        /// <summary>
        /// Hàm lọc dữ liệu bản ghi
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Thứ tự bao nhiêu</param>
        /// <param name="searchText">Chuỗi lọc</param>
        /// <returns>Các bản ghi lọc theo các tiêu chí trên</returns>
        /// Created by: TTANH (18/07/2024) 
        [HttpGet("filter")]
        public async Task<IActionResult> FiltersAsync([FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string? searchText)
        {
            await ValidateBeforeActionBase(BaseAction.Filter);
            var filterData = await _readOnlyService.FilterAsync(pageSize, pageNumber, searchText);

            return StatusCode(StatusCodes.Status200OK, filterData);
        }
        
        protected virtual async Task ValidateBeforeActionBase(BaseAction action) {}
        #endregion
    }
}