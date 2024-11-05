using Base.Model;

namespace Predict.Model
{
    /// <summary>
    /// Đối tượng để lưu trữ thông tin layout của phân hệ nhân viên
    /// </summary>
    /// CreatedBy: TTANH (02/08/2024)
    public class Predict : BaseModel, IEntityHasKey
    {
        #region Properties
        /// <summary>
        /// Khóa chính theo Guid.
        /// </summary>
        public Guid PredictId { get; set; }

        /// <summary>
        /// </summary>
        public string? ProvinceId { get; set; }

        /// <summary>
        /// </summary>
        public string? ProvinceName { get; set; }

        /// <summary>
        /// </summary>
        public string? DistrictId { get; set; }

        /// <summary>
        /// </summary>
        public string? DistrictName { get; set; }

        /// <summary>
        /// </summary>
        public string? WardId { get; set; }

        /// <summary>
        /// </summary>
        public string? WardName { get; set; }

        /// <summary>
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Thời gian vụ này bắt đầu
        /// </summary>
        public DateTime? CurrentStartDate { get; set; }

        /// <summary>
        /// Thời gian vụ này kết thúc
        /// </summary>
        public DateTime? CurrentEndDate { get; set; }

        /// <summary>
        /// Thời gian vụ trước kết thúc
        /// </summary>
        public DateTime? PreviousEndDate { get; set; }

        /// <summary>
        /// Mức độ cảnh báo của vụ trước
        /// </summary>
        public Guid? PreviousLevelWarningId { get; set; }

        /// <summary>
        /// Tên mức độ cảnh báo của vụ trước
        /// </summary>
        public string? PreviousLevelWarningName { get; set; }

        /// <summary>
        /// Trạng thái của cây trồng vụ này
        /// </summary>
        public Guid? CropId { get; set; }

        /// <summary>
        /// Trạng thái của cây trồng vụ này
        /// </summary>
        public string? CropName { get; set; }

        /// <summary>
        /// Trạng thái của cây trồng vụ này
        /// </summary>
        public Guid? PestId { get; set; }

        /// <summary>
        /// Trạng thái của cây trồng vụ này
        /// </summary>
        public string? PestName { get; set; }

        /// <summary>
        /// Trạng thái của cây trồng vụ này
        /// </summary>
        public Guid? CropStageId { get; set; }

        /// <summary>
        /// Tên trạng thái của cây trồng vụ này
        /// </summary>
        public string? CropStageName { get; set; }

        /// <summary>
        /// Trạng thái của sâu bệnh vụ này
        /// </summary>
        public Guid? PestStageId { get; set; }

        /// <summary>
        /// Tên trạng thái của sâu bệnh vụ này
        /// </summary>
        public string? PestStageName { get; set; }

        /// <summary>
        /// Trạng thái của cảnh báo vụ này
        /// </summary>
        public Guid? LevelWarningId { get; set; }

        /// <summary>
        /// Tên trạng thái của cảnh báo vụ này
        /// </summary>
        public string? LevelWarningName { get; set; }

        /// <summary>
        /// Vụ này đã kết thúc hay chưa
        /// </summary>
        public bool SeasonEnd { get; set; }

        /// <summary>
        /// Vụ này đã kết thúc hay chưa
        /// </summary>
        public string? DailyForecast { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra key của phần tử
        /// </summary>
        /// CreatedBy: TTANH (18/07/2024)
        public Guid GetKey()
        {
            return PredictId;
        }

        /// <summary>
        /// Hàm set key cho phần tử
        /// </summary>
        /// <param name="key">giá trị của key</param>
        /// CreatedBy: TTANH (18/07/2024)
        public void SetKey(Guid key)
        {
            PredictId = key;
        }
        #endregion
    }
}