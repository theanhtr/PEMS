﻿namespace PEMS.Application
{
    /// <summary>
    /// Base để xử lý đối tượng có thêm mã code
    /// </summary>
    /// CreatedBy: TTANH (18/07/2024)
    public interface ICodeService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        /// <summary>
        /// Hàm lấy một bản ghi theo mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: TTANH (18/07/2024)
        Task<TEntityDto> GetByCodeAsync(string code);

        /// <summary>
        /// Hàm lấy một mã code mới không trùng
        /// </summary>
        /// <returns>Mã code mới</returns>
        /// Created by: TTANH (18/07/2024)
        Task<string> GetNewCodeAsync();
    }
}
