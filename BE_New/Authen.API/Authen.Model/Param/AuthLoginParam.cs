﻿using System.ComponentModel.DataAnnotations;

namespace Authen.Model
{
    public class AuthLoginParam
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
        #endregion
    }
}