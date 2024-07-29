using System.ComponentModel.DataAnnotations;

namespace PEMS.Domain
{
    public class UserLogin
    {
        #region Fields
        /// <summary>
        /// Số thứ tự cột của tiêu đề
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Tên của cột trong server
        /// </summary>
        [Required]
        public string Password { get; set; }
        #endregion
    }
}
