namespace PEMS.Domain
{
    /// <summary>
    /// Interface để tương tác với DB Predict
    /// </summary>
    /// Created by: TTANH (02/08/2024)
    public interface IPredictRepository : ICodeRepository<Predict>
    {
        Task<PredictFilterResult> FiltersPredictAsync(int ProvinceId, int DistrictId, int WardId, DateTime? StartDate, DateTime? EndDate, int CropStateId, int PestLevelId, int SeasonType, int? PageSize, int? PageNumber);
    }
}
