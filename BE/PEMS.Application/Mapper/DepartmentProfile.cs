using AutoMapper;
using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Set ánh xạ dto cho department
    /// </summary>
    /// Created by: TTANH (12/07/2023)
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
