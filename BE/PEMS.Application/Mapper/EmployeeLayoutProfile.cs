using AutoMapper;
using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Set ánh xạ dto cho employee layout
    /// </summary>
    /// Created by: TTANH (02/08/2024)
    public class EmployeeLayoutProfile : Profile
    {
        public EmployeeLayoutProfile()
        {
            CreateMap<EmployeeLayout, EmployeeLayoutDto>();
            CreateMap<EmployeeLayoutCreateDto, EmployeeLayout>();
            CreateMap<EmployeeLayoutUpdateDto, EmployeeLayout>();
        }
    }
}
