using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataServices : IUserDataCRUDServices<UserData.UserDataResponse, UserDataDTO, UserDataInsertDTO, UserData.Address, AdressDataInsertDTO>
    {
        private UserDataContext _userDataContext;

        public UserDataServices(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
        }

        public async Task<IEnumerable<UserData.UserDataResponse>> AsignDataClient(IEnumerable<UserData.UserDataResponse> userDataResponse)
        {
            foreach (var userData in userDataResponse)
            {
                userData.Id = 0;
                await _userDataContext.UserDataResponse.AddAsync(userData);
                await _userDataContext.Address.AddAsync(userData.Address);
            }
            await _userDataContext.SaveChangesAsync();

            return _userDataContext.UserDataResponse;
        }
        public async Task<IEnumerable<UserData.UserDataResponse>> AddUserData(UserDataInsertDTO userDataInsertDTO)
        {
            var userData = new UserData.UserDataResponse
            {
                Name = userDataInsertDTO.Name,
                Username = userDataInsertDTO.Username,
                Email = userDataInsertDTO.Email,
                IdAdress = userDataInsertDTO.IdAdress,
                Phone = userDataInsertDTO.Phone,
                Website = userDataInsertDTO.Website
            };
        
            await _userDataContext.UserDataResponse.AddAsync(userData);
            await _userDataContext.SaveChangesAsync();
        
            return _userDataContext.UserDataResponse;
        }
        public async Task<IEnumerable<UserData.Address>> AddAdressData(AdressDataInsertDTO AdressDataInsertDTO)
        {
            var insertedAddress = new UserData.Address
            {
                Street = AdressDataInsertDTO.Street,
                Suite = AdressDataInsertDTO.Suite,
                City = AdressDataInsertDTO.City,
                Zipcode = AdressDataInsertDTO.Zipcode
            };
        
            await _userDataContext.Address.AddAsync(insertedAddress);
            await _userDataContext.SaveChangesAsync();
        
            return _userDataContext.Address;
        }
        public async Task<IEnumerable<UserDataDTO>> ReadAllUserDataList()
        {
            var userDataReadDTO = await _userDataContext.UserDataResponse.Select(userData => new UserDataDTO
            {
                Name = userData.Name,
                Email = userData.Email,
                Website = userData.Website
            }).ToListAsync();

            return userDataReadDTO;
        }
        public async Task<UserDataDTO> ReadUserData(int id)
        {
            var userData = await _userDataContext.UserDataResponse.FirstOrDefaultAsync(u => u.Id == id);

            var userDataReadDTO = new UserDataDTO
            {
                Id = id,
                Name = userData.Name,
                Email = userData.Email,
                Website = userData.Website
            };

            return userDataReadDTO;
        }
        public async Task<UserDataDTO> UpdateUserData(UserDataDTO userDataReadDTO)
        {
            var userData = await _userDataContext.UserDataResponse.FirstOrDefaultAsync(u => u.Id == userDataReadDTO.Id);

            userData.Name = userDataReadDTO.Name;
            userData.Email = userDataReadDTO.Email;
            userData.Website = userDataReadDTO.Website;
            await _userDataContext.SaveChangesAsync();

            return userDataReadDTO;
        }
        public async Task<bool> RemoveData(int id)
        {
            var userData = _userDataContext.UserDataResponse.FirstOrDefault(x => x.Id == id);

            if (userData != null)
            {
                _userDataContext.UserDataResponse.Remove(userData);
                await _userDataContext.SaveChangesAsync();
            }
            return Convert.ToBoolean(userData);
        }
        public async Task<bool> RemoveOtherData(int id)
        {
            var userData = _userDataContext.UserDataResponse.FirstOrDefault(x => x.IdAdress == id);
            var adress = _userDataContext.Address.FirstOrDefault(x => x.IdAdress == id);
            bool isAdressRemoved = userData == null && adress != null;

            if (isAdressRemoved) 
            {
                _userDataContext.Address.Remove(adress);
                await _userDataContext.SaveChangesAsync();
            }
            return isAdressRemoved;
        }
        public async Task<bool> ClearAllUserDataClient()
        {
            bool isUserDataRemoved = _userDataContext.UserDataResponse.Any();
            bool isAdressDataRemoved = _userDataContext.Address.Any();
            bool isRemoveData = isUserDataRemoved || isAdressDataRemoved;

            if (isUserDataRemoved)
            {
                _userDataContext.UserDataResponse.RemoveRange(_userDataContext.UserDataResponse);
            }
            if (isAdressDataRemoved)
            {
                _userDataContext.Address.RemoveRange(_userDataContext.Address);
            }
            if (isRemoveData)
            {
                await _userDataContext.SaveChangesAsync();
            }
            return isRemoveData;
        }
    }
}
