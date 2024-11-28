using Dapper;
using Base.DL;
using Base.Model;
using Predict.Model;
using System.Data;
using System.Web.WebPages;

namespace Predict.DL
{
    /// <summary>
    /// Triển khai bằng dapper và mysql
    /// </summary>
    /// Created By: TTANH (02/08/2024)
    public class PredictRepository : BaseRepository<Model.Predict>, IPredictRepository
    {
        #region Constructor
        public PredictRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Methods
        public async Task<int?> EndSeasonAsync(Guid? PredictId)
        {
            var sql = "UPDATE predict SET SeasonEnd = true, CurrentEndDate = @CurrentEndDate WHERE PredictId = @PredictId";
            var predict = _unitOfWork.Connection.QueryFirstOrDefault(sql, new { CurrentEndDate = DateTime.Now, PredictId = PredictId }, commandType: CommandType.Text);
            return predict;
        }

        public async Task<Model.Predict> GetPredictByLocation(PredictByLocationParam predictByLocationParam)
        {
            var procedure = $"Proc_Predict_GetPredictByLocation";

            var parameters = new DynamicParameters();
            parameters.Add("@v_ProvinceId", predictByLocationParam.ProvinceId);
            parameters.Add("@v_DistrictId", predictByLocationParam.DistrictId);
            parameters.Add("@v_WardId", predictByLocationParam.WardId);
            parameters.Add("@v_SeasonEnd", predictByLocationParam.SeasonEnd);
            parameters.Add("@v_CropId", predictByLocationParam.CropId);
            parameters.Add("@v_PestId", predictByLocationParam.PestId);

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Model.Predict>(procedure, parameters, commandType: CommandType.StoredProcedure);
        }


        public async Task<PredictFilterResult> FiltersPredictAsync(PredictFilterParam predictFilterParam, DateTime? StartDate, DateTime? EndDate, int? PageSize, int? PageNumber)
        {
            var procedure = $"Proc_Predict_Filter";

            var parameters = new DynamicParameters();
            parameters.Add("@v_ProvinceId", predictFilterParam.ProvinceId);
            parameters.Add("@v_DistrictId", predictFilterParam.DistrictId);
            parameters.Add("@v_WardId", predictFilterParam.WardId);
            parameters.Add("@v_StartDate", StartDate);
            parameters.Add("@v_EndDate", EndDate);
            parameters.Add("@v_CropStageId", predictFilterParam.CropStageId);
            parameters.Add("@v_PestStageId", predictFilterParam.PestStageId);
            parameters.Add("@v_SeasonEnd", predictFilterParam.SeasonEnd);
            parameters.Add("@v_CropId", predictFilterParam.CropId);
            parameters.Add("@v_PestId", predictFilterParam.PestId);
            parameters.Add("@v_PageSize", PageSize);
            parameters.Add("@v_PageNumber", PageNumber);

            IEnumerable<Model.Predict> predicts = null;
            TotalResult total = null;

            using (var results = await _unitOfWork.Connection.QueryMultipleAsync(procedure, parameters, commandType: CommandType.StoredProcedure))
            {
                predicts = await results.ReadAsync<Model.Predict>();
                total = await results.ReadSingleAsync<TotalResult>();
            }

            PredictFilterResult predictFilterResult = new PredictFilterResult
            {
                Predicts = predicts,
                Total = total.Total
            };

           return predictFilterResult;
        }

        public async Task<int?> DailyForecastAsync(DailyForecastParam dailyForecastParam)
        {
            var sql = "UPDATE predict SET DailyForecast = @DailyForecast," +
                "CropStageName = @CropStageName," +
                "PestStageName = @PestStageName," +
                "LevelWarningName = @LevelWarningName " +
                "WHERE PredictId = @PredictId";
            var predict = _unitOfWork.Connection.QueryFirstOrDefault(sql, 
                new { 
                    dailyForecastParam.DailyForecast, 
                    dailyForecastParam.PredictId, 
                    dailyForecastParam.CropStageName, 
                    dailyForecastParam.LevelWarningName, 
                    dailyForecastParam.PestStageName }, 
                commandType: CommandType.Text);
            return predict;
        }
        #endregion

        #region Enum
        public async Task<IEnumerable<Crop>> CropAsync()
        {
            var sql = "SELECT * FROM crop";
            
            return await _unitOfWork.Connection.QueryAsync<Crop>(sql);
        }

        public async Task<IEnumerable<Pest>> PestAsync()
        {
            var sql = "SELECT * FROM pest";
            return await _unitOfWork.Connection.QueryAsync<Pest>(sql);
        }


        public async Task<IEnumerable<CropStage>> CropStageAsync(Guid cropId)
        {
            var sql = "SELECT * FROM crop_stage WHERE CropId = @CropId ORDER BY StageOrder";
            return await _unitOfWork.Connection.QueryAsync<CropStage>(sql, new { CropId = cropId });
        }

        public async Task<IEnumerable<PestStage>> PestStageAsync(Guid pestId)
        {
            var sql = "SELECT * FROM pest_stage WHERE PestId = @PestId ORDER BY StageOrder";
            return await _unitOfWork.Connection.QueryAsync<PestStage>(sql, new { PestId = pestId });
        }

        public async Task<IEnumerable<LevelWarning>> LevelWarningAsync(Guid pestId, Guid cropId)
        {
            var sql = "SELECT * FROM level_warning WHERE PestId = @PestId AND CropId = @CropId";
            return await _unitOfWork.Connection.QueryAsync<LevelWarning>(sql, new { PestId = pestId, CropId = cropId });
        }
        #endregion
    }
}
