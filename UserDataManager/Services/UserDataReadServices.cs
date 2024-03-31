using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Repository.Class;
using UserDataManager.Repository.Interface;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataReadServices: IReadDataServices<UserDataDTO>
    {
        private IRepository<UserData.UserDResp, UserData.Address> _userDataRepository;
        private IMapper _mapper;

        public UserDataReadServices(IRepository<UserData.UserDResp, UserData.Address> userDataRepository, IMapper mapper)
        {
            _userDataRepository = userDataRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDataDTO>> ReadAllData()
        {
            var userDataResult = await _userDataRepository.ReadAllUserData();
            var userDataDTO = _mapper.Map<IEnumerable<UserDataDTO>>(userDataResult);

            return userDataDTO;
        }
        public async Task<UserDataDTO> ReadData(int id)
        {
            var userData = await _userDataRepository.ReadUserData(id);
            var userDataReadDTO = _mapper.Map<UserDataDTO>(userData);

            return userDataReadDTO;
        }
    }
}
