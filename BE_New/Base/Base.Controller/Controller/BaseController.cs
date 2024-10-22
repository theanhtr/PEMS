﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Base.BL;
using Base.Model;

namespace Base.Controller
{
    /// <summary>
    /// Controller base gồm CRUD với id
    /// </summary>
    /// CreatedBy: TTANH (18/07/2024)
    [ApiController]
    public class BaseController<TEntity> : ReadOnlyController<TEntity>
    {
        #region Fields
        private readonly IBaseService<TEntity> _baseService;
        #endregion

        #region Constructor
        public BaseController(IBaseService<TEntity> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm thêm bản ghi
        /// </summary>
        /// <param name="entityCreateDto">dữ liệu bản ghi được thêm</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// CreatedBy: TTANH (15/07/2024)
        [HttpPost]
        public virtual async Task<IActionResult> InsertAsync([FromBody] TEntity entityCreateDto)
        {
            await ValidateBeforeActionBase(BaseAction.Create);
            var result = await _baseService.InsertAsync(entityCreateDto);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Hàm update thông tin bản ghi
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <param name="entityUpdateDto">dữ liệu update</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// CreatedBy: TTANH (15/07/2024)
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TEntity entityUpdateDto)
        {
            await ValidateBeforeActionBase(BaseAction.Update);
            var result = await _baseService.UpdateAsync(id, entityUpdateDto);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Hàm xóa bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (15/07/2024)
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            await ValidateBeforeActionBase(BaseAction.Delete);
            var result = await _baseService.DeleteAsync(id);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">List các id của bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: TTANH (17/07/2024)
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteMultipleAsync([FromBody] List<Guid> ids)
        {
            await ValidateBeforeActionBase(BaseAction.Delete);
            var result = await _baseService.DeleteMultipleAsync(ids);

            return StatusCode(StatusCodes.Status200OK, result);
        }
        #endregion
    }
}
