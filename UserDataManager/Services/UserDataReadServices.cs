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
        private UserDataContext _userDataContext;
        private IRepository<UserData.UserDataResponse, UserData.Address> _userDataRepository;

        public UserDataReadServices(UserDataContext userDataContext, IRepository<UserData.UserDataResponse, UserData.Address> userDataRepository)
        {
            _userDataContext = userDataContext;
            _userDataRepository = userDataRepository;
        }

        public async Task<IEnumerable<UserDataDTO>> ReadAllUserDataList()
        {
            var userDataResult = await _userDataRepository.ReadAllUserDataList();

            var userDataDTO = userDataResult.Select(userData => new UserDataDTO
            {
                Name = userData.Name,
                Email = userData.Email,
                Website = userData.Website
            }).ToList();

            return userDataDTO;
        }
        public async Task<UserDataDTO> ReadUserData(int id)
        {
            var userData = await _userDataRepository.ReadUserData(id);

            var userDataReadDTO = new UserDataDTO
            {
                Id = userData.Id,
                Name = userData.Name,
                Email = userData.Email,
                Website = userData.Website
            };

            return userDataReadDTO;
        }
    }
}
