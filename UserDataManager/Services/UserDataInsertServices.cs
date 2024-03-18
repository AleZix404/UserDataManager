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
    public class UserDataInsertServices : IDataInsertServices<UserData.UserDataResponse, UserDataInsertDTO, UserData.Address, AdressDataInsertDTO>
    {
        private UserDataContext _userDataContext;

        public UserDataInsertServices(UserDataContext userDataContext)
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
    }
}
