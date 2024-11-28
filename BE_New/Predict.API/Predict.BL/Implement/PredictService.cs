using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Predict.DL;
using Predict.Model;
using Base.BL;
using Base.Model;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Predict.BL
{
    /// <summary>
    /// Class triển khai predict service được gọi từ controller
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public class PredictService : BaseService<Model.Predict>, IPredictService
    {
        IPredictRepository _predictRepository;
        private string? reportApi = "";
        private string? autoCalculationApi = "";
        private IConfiguration? _config;

        #region Constructor
        public PredictService(IConfiguration config, IPredictRepository predictRepository, IHttpContextAccessor httpContextAccessor) : base(predictRepository, httpContextAccessor)
        {
            _config = config;
            reportApi = _config?.GetSection("ApiUrl:ReportApi")?.Value;
            autoCalculationApi = _config?.GetSection("ApiUrl:AutoCalculationApi")?.Value;
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

        /// <summary>
        /// gọi sang Api báo cáo kiểm tra nếu chưa có báo cáo trong 60 ngày gần đây với địa điểm chỉ định thì báo lỗi
        /// lấy ra báo cáo cuối cùng
        /// </summary>
        /// <param name="predict"></param>
        /// <returns></returns>
        private async Task<Report> GetLatestReport(Model.Predict predict)
        {
            Report report = null;
            var urlReportFilter = $"{reportApi}/Report/latest";
            var reportFilterParam = new
            {
                predict.ProvinceId,
                predict.DistrictId,
                predict.WardId,
                predict.PestId
            };

            using (var client = new HttpClient())
            {
                // Serialize reportFilterParam to JSON
                var jsonContent = JsonConvert.SerializeObject(reportFilterParam);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send POST request
                var response = await client.PostAsync(urlReportFilter, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize response content to List<Report>
                    var responseContent = await response.Content.ReadAsStringAsync();
                    report = JsonConvert.DeserializeObject<Report>(responseContent);
                }
                else
                {
                    // Xử lý lỗi nếu cần
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }
            }

            return report;
        }

        public async Task CheckPredictReport(Model.Predict predict)
        {
            // kiểm tra xem đã tồn tại dự báo nào chưa kết thúc mùa vụ không, nếu có thì báo lỗi
            var existPredict = await _predictRepository.GetPredictByLocation(
                new PredictByLocationParam
                {
                    ProvinceId = predict.ProvinceId,
                    DistrictId = predict.DistrictId,
                    WardId = predict.WardId,
                    SeasonEnd = false,
                    CropId = predict.CropId,
                    PestId = predict.PestId
                }
             );

            if (existPredict != null)
            {
                throw new ValidateException(StatusErrorCode.DataWrong, "Đang tồn tại dự báo tại khu vực này mà chưa kết thúc mùa vụ", "");
            }

            // nếu dự báo quá 60 ngày thì không cho tạo
            if (predict.CurrentStartDate < DateTime.Now.AddDays(-60))
            {
                throw new ValidateException(StatusErrorCode.DataWrong, "Thời điểm bắt đầu vụ này không được cách hiện tại quá 60 ngày", "");
            }

            var latestReport = await GetLatestReport(predict);

            if (latestReport == null || latestReport.PestId != predict.PestId)
            {
                throw new ValidateException(StatusErrorCode.DataWrong, "Không tìm thấy báo cáo của khu vực", "");
            }

            if (latestReport.ModifiedDate < DateTime.Now.AddDays(-60))
            {
                throw new ValidateException(StatusErrorCode.DataWrong, "Báo cáo cuối cùng của khu vực cách hiện tại quá 60 ngày", "");
            }
        }

        public override async Task BeforeInsertAsync(Model.Predict entityCreateDto)
        {
            await CheckPredictReport(entityCreateDto);

            entityCreateDto.SeasonEnd = false;

            // dự báo thời gian kết thúc
            if (entityCreateDto.CropId != null)
            {
                var cropStages = await _predictRepository.CropStageAsync(entityCreateDto.CropId.Value);
                int totalDuration = 0;

                foreach (var cropStage in cropStages)
                {
                    totalDuration += cropStage.DevelopmentTime;
                }
                
                entityCreateDto.CurrentEndDate = entityCreateDto.CurrentStartDate.Value.AddDays(totalDuration);
            }
        }

        public async Task TriggerAutoCalculation(Model.Predict predict)
        {
            var urlAutoCal = $"{autoCalculationApi}/auto-calculate";

            using (var client = new HttpClient())
            {
                // Serialize reportFilterParam to JSON
                var jsonContent = JsonConvert.SerializeObject(predict);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send POST request
                var response = await client.PostAsync(urlAutoCal, httpContent);
            }
        }

        /// <summary>
        /// Trigger sang tự động tính toán
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        public override async Task AfterInsertAsync(Model.Predict entityCreateDto)
        {
            await TriggerAutoCalculation(entityCreateDto);
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



            var predictFilterResult = await _predictRepository.FiltersPredictAsync(predictFilterParam, startDate, endDate, pageSize, pageNumber);

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

        public async Task<int?> DailyForecastAsync(DailyForecastParam dailyForecastParam)
        {
            var result = await _predictRepository.DailyForecastAsync(dailyForecastParam);

            return 0;
        }
        #endregion

        #region Enum
        public async Task<IEnumerable<Crop>> CropAsync()
        {
            return await _predictRepository.CropAsync();
        }

        public async Task<IEnumerable<Pest>> PestAsync()
        {
            return await _predictRepository.PestAsync();
        }


        public async Task<IEnumerable<CropStage>> CropStageAsync(Guid cropId)
        {
            return await _predictRepository.CropStageAsync(cropId);
        }

        public async Task<IEnumerable<PestStage>> PestStageAsync(Guid pestId)
        {
            return await _predictRepository.PestStageAsync(pestId);
        }

        public async Task<IEnumerable<LevelWarning>> LevelWarningAsync(Guid pestId, Guid cropId)
        {
            return await _predictRepository.LevelWarningAsync(pestId, cropId);
        }
        #endregion
    }
}
