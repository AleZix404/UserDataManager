using Microsoft.EntityFrameworkCore;
using System.Collections;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Repository.Interface;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataUpdateServices : IDataUpdateServices<UserData.UserDataResponse, UserDataDTO>
    {
        IRepository<UserData.UserDataResponse, UserData.Address> _userDataRepository;

        public UserDataUpdateServices(IRepository<UserData.UserDataResponse, UserData.Address> userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        public async Task<UserDataDTO> UpdateUserData(UserData.UserDataResponse userData)
        {
            var userDataResult = await _userDataRepository.UpdateUserData(userData);

            var userDataDTO = new UserDataDTO 
            {
                Id = userDataResult.Id,
                Name = userDataResult.Name,
                Email = userDataResult.Email,
                Website = userDataResult.Website
            };

            return userDataDTO;
        }
    }
}
