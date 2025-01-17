﻿using AutoMapper;
using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Set ánh xạ dto cho employee
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<EmployeeExcelDto, Employee>();
            CreateMap<EmployeeExcelDto, EmployeeCreateDto>();
            CreateMap<EmployeeExcelDto, EmployeeUpdateDto>();
        }
    }
}
