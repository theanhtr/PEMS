using AutoMapper;
using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Set ánh xạ dto cho employee layout
    /// </summary>
    /// Created by: TTANH (02/08/2024)
    public class PredictProfile : Profile
    {
        public PredictProfile()
        {
            CreateMap<Predict, PredictDto>();
            CreateMap<PredictCreateDto, Predict>();
            CreateMap<PredictUpdateDto, Predict>();
        }
    }
}
