using Dapper;
using PEMS.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using static Dapper.SqlMapper;

namespace PEMS.Infrastructure
{
    /// <summary>
    /// Triển khai bằng dapper và mysql
    /// </summary>
    /// Created By: TTANH (02/08/2024)
    public class PredictRepository : CodeRepository<Predict>, IPredictRepository
    {
        #region Constructor
        public PredictRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Methods
        public async Task<PredictFilterResult> FiltersPredictAsync(int ProvinceId, int DistrictId, int WardId, DateTime? StartDate, DateTime? EndDate, int CropStateId, int PestLevelId, int SeasonType, int? PageSize, int? PageNumber)
        {
            var procedure = $"Proc_Predict_Filter";

            var parameters = new DynamicParameters();
            parameters.Add("@v_ProvinceId", ProvinceId);
            parameters.Add("@v_DistrictId", DistrictId);
            parameters.Add("@v_WardId", WardId);
            parameters.Add("@v_StartDate", StartDate);
            parameters.Add("@v_EndDate", EndDate);
            parameters.Add("@v_CropStateId", CropStateId);
            parameters.Add("@v_PestLevelId", PestLevelId);
            parameters.Add("@v_SeasonType", SeasonType);
            parameters.Add("@v_PageSize", PageSize);
            parameters.Add("@v_PageNumber", PageNumber);

            IEnumerable<Predict> predicts = null;
            TotalResult total = null;

            using (var results = await _unitOfWork.Connection.QueryMultipleAsync("Proc_Predict_Filter", parameters, commandType: CommandType.StoredProcedure))
            {
                predicts = await results.ReadAsync<Predict>();
                total = await results.ReadSingleAsync<TotalResult>();
            }

            PredictFilterResult predictFilterResult = new PredictFilterResult
            {
                Predicts = predicts,
                Total = total.Total
            };

           return predictFilterResult;
        }
        #endregion
    }
}
