namespace PEMS.Application
{
    public class EmployeeFilterResponse : BaseFilterResponse<EmployeeDto>
    {
        public EmployeeFilterResponse(int? totalPage, int? totalRecord, int? currentPage, int? currentPageRecords, List<EmployeeDto>? data) : base(totalPage, totalRecord, currentPage, currentPageRecords, data)
        {
        }
    }
}
