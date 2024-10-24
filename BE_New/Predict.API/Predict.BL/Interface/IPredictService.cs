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
    }
}
