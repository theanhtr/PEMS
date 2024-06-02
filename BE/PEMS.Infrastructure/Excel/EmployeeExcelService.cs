using AutoMapper;
using PEMS.Domain;
using PEMS.Application;

namespace PEMS.Infrastructure
{
    public class EmployeeExcelService : ExcelService<Employee, EmployeeDto, EmployeeExcelDto, EmployeeLayoutDto>
    {
        public EmployeeExcelService(IEmployeeRepository employeeRepository, IMapper mapper, IExcelWorker<EmployeeDto, EmployeeExcelDto, EmployeeLayoutDto> employeeExcelWorker) : base(employeeRepository, mapper, employeeExcelWorker)
        {
        }
    }
}
