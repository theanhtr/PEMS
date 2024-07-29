using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Interface service cho employee layout để controller gọi đến
    /// </summary>
    /// Created by: TTANH (02/08/2024)
    public interface IEmployeeLayoutService : IBaseService<EmployeeLayout, EmployeeLayoutDto, EmployeeLayoutCreateDto, EmployeeLayoutUpdateDto>
    {
        /// <summary>
        /// Hàm cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="employeeLayoutsUpdate">Dữ liệu của các bản ghi cập nhật</param>
        /// <returns>Số hàng bị ảnh hưởng</returns>
        /// Created by: TTANH (02/08/2024)
        Task<int> UpdateMultipleAsync(List<EmployeeLayoutUpdateDto> employeeLayoutsUpdate);

        /// <summary>
        /// Hàm lấy lại dữ liệu mặc định
        /// </summary>
        /// CreatedBy: TTANH (03/08/2024)
        Task SetDefault();
    }
}
