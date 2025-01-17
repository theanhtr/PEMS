﻿namespace PEMS.Domain
{
    /// <summary>
    /// Model để lấy ra dữ liệu cho việc setting data
    /// </summary>
    /// CreatedBy: TTANH (17/07/2024)
    public class ExcelImportSettingData
    {
        #region Property
        /// <summary>
        /// Các sheet có trong file excel
        /// </summary>
        public List<string> Sheets { get; set; }
        
        /// <summary>
        /// Dòng chứa tiêu đề
        /// </summary>
        public int HeaderRowIndex { get; set; }
        #endregion
    }
}
