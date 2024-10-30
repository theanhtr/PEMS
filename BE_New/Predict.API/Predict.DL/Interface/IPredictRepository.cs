using Base.DL;
using Predict.Model;

namespace Predict.DL
{
    /// <summary>
    /// Interface để tương tác với DB Predict
    /// </summary>
    /// Created by: TTANH (02/08/2024)
    public interface IPredictRepository : IBaseRepository<Model.Predict>
    {
        Task<PredictFilterResult> FiltersPredictAsync(string? ProvinceId, string? DistrictId, string? WardId, DateTime? StartDate, DateTime? EndDate, int CropStageId, int PestStageId, bool SeasonEnd, int? PageSize, int? PageNumber);
        Task<int?> EndSeasonAsync(Guid? PredictId);

        Task<IEnumerable<Crop>> CropAsync();
        Task<IEnumerable<Pest>> PestAsync();
        Task<IEnumerable<CropStage>> CropStageAsync(Guid cropId);
        Task<IEnumerable<PestStage>> PestStageAsync(Guid pestId);
        Task<IEnumerable<LevelWarning>> LevelWarningAsync(Guid pestId, Guid cropId);
    }
}
