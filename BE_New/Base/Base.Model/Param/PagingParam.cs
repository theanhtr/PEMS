using System.ComponentModel.DataAnnotations;

namespace Base.Model
{
    public class PagingParam
    {
        #region Fields
        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int? PageNumber { get; set; }
        #endregion
    }
}
