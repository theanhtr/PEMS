namespace PEMS.Domain
{
    public class EmployeeLayoutValidate : BaseValidate<EmployeeLayout>, IEmployeeLayoutValidate
    {
        public EmployeeLayoutValidate(IEmployeeLayoutRepository employeeLayoutRepository) : base(employeeLayoutRepository)
        {
        }
    }
}
