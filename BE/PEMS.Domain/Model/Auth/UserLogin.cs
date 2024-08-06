using System.ComponentModel.DataAnnotations;

namespace PEMS.Domain
{
    public class UserLogin
    {
        #region Fields
        /// <summary>
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// </summary>
        [Required]
        public string Password { get; set; }
        #endregion
    }
}
