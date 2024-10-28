using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Predict.DL;
using Predict.Model;
using Base.BL;
using Base.Model;

namespace Predict.BL
{
    /// <summary>
    /// Class triển khai predict service được gọi từ controller
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public class PredictService : BaseService<Model.Predict>, IPredictService
    {
        IPredictRepository _predictRepository;

        #region Constructor
        public PredictService(IPredictRepository predictRepository, IHttpContextAccessor httpContextAccessor) : base(predictRepository, httpContextAccessor)
        {
            _predictRepository = predictRepository;
        }
        #endregion

        #region Methods
        public async Task<int?> EndSeasonAsync(Guid? PredictId)
        {
            if (PredictId == null)
            {
                throw new ValidateException();
            }

            var predict = await _predictRepository.GetByIdAsync(PredictId.Value);

            if (predict == null)
            {
                throw new NotFoundException();
            }

            return await _predictRepository.EndSeasonAsync(PredictId.Value);
        }

        public override Task BaseServiceMoreProcessInsertAsync(Model.Predict entityCreateDto)
        {
            entityCreateDto.SeasonEnd = false;
            return base.BaseServiceMoreProcessInsertAsync(entityCreateDto);
        }

        public async Task<BaseFilterResponse<Model.Predict>> FiltersPredictAsync([FromBody] PredictFilterParam predictFilterParam)
        {
            // xử lý param trước khi gửi xuống repository
            DateTime? startDate = predictFilterParam.StartDate?.Date;
            DateTime? endDate = predictFilterParam.EndDate?.Date.AddDays(1).AddSeconds(-1);

            int? pageSize = predictFilterParam.PageSize;
            int? pageNumber = predictFilterParam.PageNumber;

            if (pageSize == null || pageSize <= 0) { pageSize = 999999; }
            if (pageNumber == null || pageNumber < 1) { pageNumber = 1; }



            var predictFilterResult = await _predictRepository.FiltersPredictAsync(predictFilterParam.ProvinceId, predictFilterParam.DistrictId, predictFilterParam.WardId,
                                                                        startDate, endDate, predictFilterParam.CropStateId, predictFilterParam.PestLevelId,
                                                                        predictFilterParam.SeasonEnd, pageSize, pageNumber);

            var totalRecord = predictFilterResult.Total;
            var totalPage = Convert.ToInt32(Math.Ceiling((double)totalRecord / (double)pageSize));

            if (pageNumber > totalPage)
            {
                pageNumber = totalPage;
            }


            if (predictFilterResult.Predicts == null)
            {
                throw new NotFoundException();
            }

            var currentPage = pageNumber;
            var currentPageRecords = predictFilterResult.Predicts.Count();

            var filterData = new BaseFilterResponse<Model.Predict>(totalPage, totalRecord, currentPage, currentPageRecords, predictFilterResult.Predicts.ToList());

            return filterData;
        }
        #endregion
    }
}
