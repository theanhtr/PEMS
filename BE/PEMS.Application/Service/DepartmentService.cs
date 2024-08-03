using AutoMapper;
using Microsoft.AspNetCore.Http;
using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Class triển khai department service được gọi từ controller
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public class DepartmentService : CodeService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IDepartmentValidate departmentValidate, IHttpContextAccessor httpContextAccessor) : base(departmentRepository, mapper, departmentValidate, httpContextAccessor)
        {
        }
        #endregion
    }
}
