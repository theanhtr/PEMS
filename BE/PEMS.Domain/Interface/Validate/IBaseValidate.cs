﻿using System.Text.RegularExpressions;

namespace PEMS.Domain
{
    /// <summary>
    /// Base validate chính
    /// </summary>
    /// Created by: TTANH (15/07/2024)
    public interface IBaseValidate<TEntity>
    {
        /// <summary>
        /// Hàm dùng để validate code
        /// </summary>
        /// <param name="code">Code muốn validate</param>
        /// <returns>true nếu không có lỗi</returns>
        /// <exception cref="ValidateException">Nếu có lỗi thì sẽ trả exception</exception>
        /// CreatedBy: TTANH (14/07/2024)
        Task<bool> CodeValidate(string code);
    }
}
