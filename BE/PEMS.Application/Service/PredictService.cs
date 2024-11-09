using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PEMS.Domain;
using System.Reflection;

namespace PEMS.Application
{
    /// <summary>
    /// Class triển khai predict service được gọi từ controller
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public class PredictService : BaseService<Predict, PredictDto, PredictCreateDto, PredictUpdateDto>, IPredictService
    {
        IPredictRepository _predictRepository;

        #region Constructor
        public PredictService(IPredictRepository predictRepository, IMapper mapper, IPredictValidate predictValidate, IHttpContextAccessor httpContextAccessor) : base(predictRepository, mapper, predictValidate, httpContextAccessor)
        {
            _predictRepository = predictRepository;
        }
        #endregion

        #region Methods
        public async Task<BaseFilterResponse<PredictDto>> FiltersPredictAsync([FromBody] PredictFilterParam predictFilterParam)
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
                                                                        predictFilterParam.SeasonType, pageSize, pageNumber);

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

            var predictsDto = _mapper.Map<List<PredictDto>>(predictFilterResult.Predicts);

            var filterData = new BaseFilterResponse<PredictDto>(totalPage, totalRecord, currentPage, currentPageRecords, predictsDto);

            return filterData;
        }
        #endregion
    }
}
