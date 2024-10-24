namespace Report.Model
{
    public class ReportFilterResult
    {
        public IEnumerable<Report> Reports { get; set; }

        public int Total { get; set; }
    }
}
