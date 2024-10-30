using Base.BL;
using Base.Model;
using Predict.Model;

namespace Predict.BL
{
    /// <summary>
    /// Interface service cho employee layout để controller gọi đến
    /// </summary>
    /// Created by: TTANH (02/08/2024)
    public interface IPredictService : IBaseService<Model.Predict>
    {
        Task<BaseFilterResponse<Model.Predict>> FiltersPredictAsync(PredictFilterParam predictFilterParam);
        Task<int?> EndSeasonAsync(Guid? PredictId);

        Task<IEnumerable<Crop>> CropAsync();
        Task<IEnumerable<Pest>> PestAsync();
        Task<IEnumerable<CropStage>> CropStageAsync(Guid cropId);
        Task<IEnumerable<PestStage>> PestStageAsync(Guid pestId);
        Task<IEnumerable<LevelWarning>> LevelWarningAsync(Guid pestId, Guid cropId);

    }
}
