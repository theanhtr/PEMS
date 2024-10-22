using System.ComponentModel.DataAnnotations;

namespace Authen.Model
{
    public class AuthRegisterParam
    {
        #region Fields
        /// <summary>
        /// </summary>
        [Required]
        public string? Username { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Fullname { get; set; }
        #endregion
    }
}
