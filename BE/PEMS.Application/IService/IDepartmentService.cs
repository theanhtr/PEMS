using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Interface service cho department để controller gọi đến
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public interface IDepartmentService : ICodeService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
    }
}
