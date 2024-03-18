using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Repository.Class;
using UserDataManager.Repository.Interface;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataInsertServices : IDataInsertServices<UserData.UserDataResponse, UserDataInsertDTO, UserData.Address, AddressDataInsertDTO>
    {
        private IRepository<UserData.UserDataResponse, UserData.Address> _userDataRepository;

        public UserDataInsertServices(IRepository<UserData.UserDataResponse, UserData.Address> userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        public async Task<IEnumerable<UserDataInsertDTO>> AsignDataClient(IEnumerable<UserData.UserDataResponse> userDataResponse) 
        {
            var userData = await _userDataRepository.SetDataList(userDataResponse);
            return SetInsertDataDTO(userData);
        }

        public async Task<IEnumerable<UserDataInsertDTO>> AddUserData(UserData.UserDataResponse userData)
        {
            var userDataResult = await _userDataRepository.SetUserData(userData);
            return SetInsertDataDTO(userDataResult);
        }

        private IEnumerable<UserDataInsertDTO> SetInsertDataDTO(IEnumerable<UserData.UserDataResponse> userDataResult)
        {
            return userDataResult.Select(u => new UserDataInsertDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Website = u.Website
            });
        }

        public async Task<IEnumerable<AddressDataInsertDTO>> SetAdressData(UserData.Address adressData)
        {
            var adressResult = await _userDataRepository.AddAdressData(adressData);

            var insertedAddress = adressResult.Select(u => new AddressDataInsertDTO
            {
                IdAdress = u.IdAdress,
                Street = u.Street,
                Suite = u.Suite,
                City = u.City,
                Zipcode = u.Zipcode
            });

            return insertedAddress;
        }
    }
}
