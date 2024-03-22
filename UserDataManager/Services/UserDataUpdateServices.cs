using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Repository.Interface;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataUpdateServices : IDataUpdateServices<UserDataUpdateDTO, UserDataDTO>
    {
        IRepository<UserData.UserDataResponse, UserData.Address> _userDataRepository;
        IMapper _mapper;

        public UserDataUpdateServices(IRepository<UserData.UserDataResponse, UserData.Address> userDataRepository, IMapper mapper)
        {
            _userDataRepository = userDataRepository;
            _mapper = mapper;
        }

        public async Task<UserDataDTO> UpdateData(UserDataUpdateDTO userDataUpdateDTO)
        {
            var userDataResponse = _mapper.Map<UserData.UserDataResponse>(userDataUpdateDTO);
            var userDataResult = await _userDataRepository.UpdateUserData(userDataResponse);

            if (userDataResult == null) 
            {
                throw new Exception("User Data not exists");
            }
            var userDataDTO = _mapper.Map<UserDataDTO>(userDataResult);

            return userDataDTO;
        }
    }
}
