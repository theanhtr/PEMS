﻿namespace PEMS.Domain
{
    /// <summary>
    /// Interface để tương tác với DB EmployeeLayout
    /// </summary>
    /// Created by: TTANH (02/08/2024)
    public interface IEmployeeLayoutRepository : ICodeRepository<EmployeeLayout>
    {
        /// <summary>
        /// Hàm cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="employeeLayoutsStrConvert">Dữ liệu lại thành chuỗi để truyền vào truy vấn</param>
        /// CreatedBy: TTANH (02/08/2024)
        Task<int> UpdateMultipleAsync(string employeeLayoutsStrConvert);

        /// <summary>
        /// Hàm lấy lại dữ liệu mặc định
        /// </summary>
        /// CreatedBy: TTANH (03/08/2024)
        Task SetDefault();
    }
}
