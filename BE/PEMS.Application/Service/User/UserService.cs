using AutoMapper;
using Microsoft.AspNetCore.Http;
using PEMS.Domain;

namespace PEMS.Application
{
    public class UserService : IUserService
    {
        IMapper _mapper;
        IUserRepository _userRepository;
        #region Constructor
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<string> CreateNewUser(string username, string hashPassword, string fullname)
        {
            var user = await _userRepository.CreateNewUser(username, hashPassword, fullname);

            return user;
        }

        public async Task<UserDto> GetUserInfo(Guid userId)
        {
            var user = _userRepository.GetByUserId(userId);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<User> GetByUserName(string username)
        {
            return _userRepository.GetByUserName(username);
        }

        public async Task<User> GetRoleID(Guid userId)
        {
            return _userRepository.GetByUserId(userId);
        }
    }
}
