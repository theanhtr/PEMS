using PEMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace PEMS.Application
{
    /// <summary>
    /// Dto trả về của thông tin
    /// </summary>
    public class PredictDto
    {
        #region Properties
        /// <summary>
        /// Khóa chính theo Guid.
        /// </summary>
        public Guid PredictId { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        public int ProvinceId { get; set; }

        /// <summary>
        /// </summary>
        public string? ProvinceName { get; set; }

        /// <summary>
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// </summary>
        public string? DistrictName { get; set; }

        /// <summary>
        /// </summary>
        public int WardId { get; set; }

        /// <summary>
        /// </summary>
        public string? WardName { get; set; }

        /// <summary>
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? CurrentStartDate { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? CurrentEndDate { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? PreviousEndDate { get; set; }

        /// <summary>
        /// </summary>
        public int PreviousLevelWarningId { get; set; }

        /// <summary>
        /// </summary>
        public int CropStateId { get; set; }

        /// <summary>
        /// </summary>
        public int PestLevelId { get; set; }

        /// <summary>
        /// </summary>
        public int LevelWarningId { get; set; }
        #endregion
    }
}
