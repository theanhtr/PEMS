using AutoMapper;
using PEMS.Domain;

namespace PEMS.Application
{
    /// <summary>
    /// Set ánh xạ dto cho UserProfile
    /// </summary>
    /// Created by: TTANH (12/07/2024)
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
