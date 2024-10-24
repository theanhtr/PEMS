using System.ComponentModel.DataAnnotations;
using Base.Model;

namespace Report.Model
{
    public class ReportFilterParam : PagingParam
    {
        #region Fields
        /// <summary>
        /// Gets or sets the province ID.
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the district ID.
        /// </summary>
        public int? DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the ward ID.
        /// </summary>
        public int? WardId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime? ReportStartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime? ReportEndDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public string? ReportName { get; set; }
        #endregion
    }
}
