using System.ComponentModel.DataAnnotations;

namespace PEMS.Domain
{
    /// <summary>
    /// Class lưu lựa chọn của tương ứng tiêu đề 
    /// với thứ tự cột trong excel
    /// client gửi tới server
    /// </summary>
    /// CreatedBy: TTANH (19/07/2024)
    public class ExcelHeaderMapColumn
    {
        #region Fields
        /// <summary>
        /// Số thứ tự cột của tiêu đề
        /// </summary>
        [Required]
        public int HeaderColumnIndex { get; set; }

        /// <summary>
        /// Tên của cột trong server
        /// </summary>
        [Required]
        public string ServerColumnName { get; set; }
        #endregion
    }
}
