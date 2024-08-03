using System.ComponentModel.DataAnnotations;

namespace PEMS.Domain
{
    /// <summary>
    /// Đối tượng để lưu trữ thông tin layout của phân hệ nhân viên
    /// </summary>
    /// CreatedBy: TTANH (02/08/2024)
    public class Predict : BaseAuditEntity, IEntityHasKey
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

        #region Methods
        /// <summary>
        /// Lấy ra key của phần tử
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        public Guid GetKey()
        {
            return PredictId;
        }

        /// <summary>
        /// Hàm set key cho phần tử
        /// </summary>
        /// <param name="key">giá trị của key</param>
        /// CreatedBy: TTANH (18/07/2024)
        public void SetKey(Guid key)
        {
            PredictId = key;
        }
        #endregion
    }
}