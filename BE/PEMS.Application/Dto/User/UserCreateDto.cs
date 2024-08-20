using PEMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace PEMS.Application
{
    /// <summary>
    /// Dto trả về của thông tin cột nhân viên
    /// </summary>
    public class UserCreateDto
    {
        #region Property
        /// <summary>
        /// Tên của cột trên server để lấy dữ liệu từ api.
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        /// <summary>
        /// Tên cột hiển thị trên màn hình bằng tiếng việt
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Fullname { get; set; }

        /// <summary>
        /// Tên cột hiển thị trên màn hình mặc định bằng tiếng việt
        /// </summary>
        [Required]
        public int RoleID { get; set; }

        /// <summary>
        /// Tên cột hiển thị trên màn hình mặc định bằng tiếng việt
        /// </summary>
        public string Password { get; set; }
        #endregion
    }
}
