using System.ComponentModel.DataAnnotations;
using Base.Model;

namespace User.Model
{
    /// <summary>
    /// Đối tượng để lưu trữ thông tin layout của phân hệ nhân viên
    /// </summary>
    /// CreatedBy: TTANH (02/08/2024)
    public class User : BaseModel, IEntityHasKey
    {
        #region Properties
        /// <summary>
        /// Khóa chính theo Guid.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Tên của cột trên server để lấy dữ liệu từ api.
        /// </summary>
        [Required]
        [StringLength(255)]
        public string? Username { get; set; }

        /// <summary>
        /// Tooltip của cột bằng tiếng việt
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Tên cột hiển thị trên màn hình bằng tiếng việt
        /// </summary>
        [Required]
        [StringLength(255)]
        public string? Fullname { get; set; }

        /// <summary>
        /// Tên cột hiển thị trên màn hình mặc định bằng tiếng việt
        /// </summary>
        [Required]
        public int RoleID { get; set; }

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

        public string? PhoneNumber { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra key của phần tử
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        public Guid GetKey()
        {
            return UserId;
        }

        /// <summary>
        /// Hàm set key cho phần tử
        /// </summary>
        /// <param name="key">giá trị của key</param>
        /// CreatedBy: TTANH (18/07/2024)
        public void SetKey(Guid key)
        {
            UserId = key;
        }
        #endregion
    }
}