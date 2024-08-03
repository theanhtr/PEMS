using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Interface service cho employee layout để controller gọi đến
    /// </summary>
    /// Created by: TTANH (02/08/2024)
    public interface IPredictService : IBaseService<Predict, PredictDto, PredictCreateDto, PredictUpdateDto>
    {
    }
}
