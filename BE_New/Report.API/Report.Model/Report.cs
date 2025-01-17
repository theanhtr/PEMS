﻿using Base.Model;

namespace Report.Model
{
    /// <summary>
    /// Đối tượng để lưu trữ thông tin layout của phân hệ nhân viên
    /// </summary>
    /// CreatedBy: TTANH (02/08/2024)
    public class Report : BaseModel, IEntityHasKey
    {
        #region Properties
        /// <summary>
        /// Khóa chính theo Guid.
        /// </summary>
        public Guid ReportId { get; set; }

        /// <summary>
        /// </summary>
        public string? ProvinceId { get; set; }

        /// <summary>
        /// </summary>
        public string? ProvinceName { get; set; }

        /// <summary>
        /// </summary>
        public string? DistrictId { get; set; }

        /// <summary>
        /// </summary>
        public string? DistrictName { get; set; }

        /// <summary>
        /// </summary>
        public string? WardId { get; set; }

        /// <summary>
        /// </summary>
        public string? WardName { get; set; }

        /// <summary>
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// </summary>
        public string? ReportName { get; set; }

        /// <summary>
        /// </summary>
        public Guid? PestStageId { get; set; }

        /// <summary>
        /// </summary>
        public string? PestStageName { get; set; }

        /// <summary>
        /// </summary>
        public Guid? PestId { get; set; }

        /// <summary>
        /// </summary>
        public string? PestName { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra key của phần tử
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        public Guid GetKey()
        {
            return ReportId;
        }

        /// <summary>
        /// Hàm set key cho phần tử
        /// </summary>
        /// <param name="key">giá trị của key</param>
        /// CreatedBy: TTANH (18/07/2024)
        public void SetKey(Guid key)
        {
            ReportId = key;
        }
        #endregion
    }
}