﻿using System.ComponentModel.DataAnnotations;

namespace PEMS.Domain
{
    public class PredictFilterParam
    {
        #region Fields
        /// <summary>
        /// Gets or sets the province ID.
        /// </summary>
        [Required]
        public int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the district ID.
        /// </summary>
        [Required]
        public int DistrictId { get; set; }

        /// <summary>
        /// Gets or sets the ward ID.
        /// </summary>
        [Required]
        public int WardId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the crop state ID.
        /// </summary>
        [Required]
        public int CropStateId { get; set; }

        /// <summary>
        /// Gets or sets the pest level ID.
        /// </summary>
        [Required]
        public int PestLevelId { get; set; }

        /// <summary>
        /// Gets or sets the season type.
        /// </summary>
        [Required]
        public int SeasonType { get; set; }

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
