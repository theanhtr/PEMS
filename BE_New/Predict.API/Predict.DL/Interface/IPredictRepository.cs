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
        Task<PredictFilterResult> FiltersPredictAsync(int ProvinceId, int DistrictId, int WardId, DateTime? StartDate, DateTime? EndDate, int CropStateId, int PestLevelId, bool SeasonEnd, int? PageSize, int? PageNumber);
        Task<int?> EndSeasonAsync(Guid? PredictId);
    }
}
