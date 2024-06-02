using Microsoft.AspNetCore.Mvc;
using PEMS.Application;
using PEMS.Domain;

namespace PEMS.Controllers
{
    /// <summary>
    /// Controller của phòng ban
    /// </summary>
    /// CreatedBy: TTANH (12/07/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : CodeController<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
        }
    }
}
