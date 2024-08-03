using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        #endregion
    }
}
