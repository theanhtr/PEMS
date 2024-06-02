namespace PEMS.Domain
{
    public class EmployeeValidate : BaseValidate<Employee>, IEmployeeValidate
    {
        public EmployeeValidate(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
        }
    }
}
