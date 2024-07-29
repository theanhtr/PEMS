using PEMS.Domain;
using System.ComponentModel.DataAnnotations;

namespace PEMS.Application
{
    /// <summary>
    /// Dto cập nhật phòng ban
    /// </summary>
    public class DepartmentUpdateDto : IEntityHasCode
    {
        #region Property
        /// <summary>
        /// Mã code của phòng ban
        /// </summary>
        [Required]
        [StringLength(20)]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên của phòng ban
        /// </summary>
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra code của phần tử
        /// </summary>
        /// CreatedBy: TTANH (25/07/2024)
        public string GetCode()
        {
            return DepartmentCode;
        }

        /// <summary>
        /// Hàm set code cho phần tử
        /// </summary>
        /// <param name="code">giá trị của code</param>
        /// CreatedBy: TTANH (25/07/2024)
        public void SetCode(string code)
        {
            DepartmentCode = code;
        }
        #endregion
    }
}
