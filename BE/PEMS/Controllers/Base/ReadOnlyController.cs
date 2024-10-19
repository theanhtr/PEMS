using Microsoft.AspNetCore.Mvc;
using PEMS.Application;
using PEMS.Domain;

namespace PEMS.Controllers
{
    /// <summary>
    /// Controller base gồm đọc và lọc với đối tượng không có code
    /// </summary>
    /// CreatedBy: TTANH (18/07/2024)
    [ApiController]
    public class ReadOnlyController<TEntity, TEntityDto> : ControllerBase
    {
        #region Fields
        private readonly IReadOnlyService<TEntity, TEntityDto> _readOnlyService;
        #endregion

        #region Constructor
        public ReadOnlyController(IReadOnlyService<TEntity, TEntityDto> readOnlyService)
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
            ValidateBeforeActionBase(BaseAction.Get);
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
            ValidateBeforeActionBase(BaseAction.Get);
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
            ValidateBeforeActionBase(BaseAction.Filter);
            var filterData = await _readOnlyService.FilterAsync(pageSize, pageNumber, searchText);

            return StatusCode(StatusCodes.Status200OK, filterData);
        }
        
        protected virtual void ValidateBeforeActionBase(BaseAction action) {}

        protected virtual void BlockAllAction(int role)
        {
            // Lấy các claim từ JWT
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var userRepository = HttpContext.RequestServices.GetService<IUserRepository>();
            var user = userRepository.GetByUserId(Guid.Parse(userId));

            if (user != null)
            {
                var roleId = user.RoleID;
                if (roleId > role)
                {
                    throw new ForbiddenException(StatusErrorCode.Forbidden, "Bạn không có quyền truy cập trang này.", null);
                }
            }
            else
            {
                throw new ForbiddenException(StatusErrorCode.Forbidden, "Bạn không có quyền truy cập trang này.", null);
            }
        }
        #endregion
    }
}