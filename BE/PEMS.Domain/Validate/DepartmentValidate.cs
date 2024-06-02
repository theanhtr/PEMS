namespace PEMS.Domain
{
    public class DepartmentValidate : BaseValidate<Department>, IDepartmentValidate
    {
        public DepartmentValidate(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
        }
    }
}
