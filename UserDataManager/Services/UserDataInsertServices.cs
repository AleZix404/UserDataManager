using AutoMapper;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Repository.Class;
using UserDataManager.Repository.Interface;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataInsertServices : IDataInsertServices<UserDataDTO, UserDataInsertDTO, AddressDTO, AddressDataInsertDTO>
    {
        private IRepository<UserData.UserDataResponse, UserData.Address> _userDataRepository;
        private IMapper _mapper;

        public UserDataInsertServices(IRepository<UserData.UserDataResponse, UserData.Address> userDataRepository, IMapper mapper)
        {
            _userDataRepository = userDataRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDataDTO>> AsignDataClient(IEnumerable<UserDataInsertDTO> userDataInsert)
        {
            var userDataResponse = _mapper.Map<IEnumerable<UserData.UserDataResponse>>(userDataInsert);
            var userData = await _userDataRepository.SetDataList(userDataResponse);
            return _mapper.Map<IEnumerable<UserDataDTO>>(userData);
        }

        public async Task<IEnumerable<UserDataDTO>> AddUserData(UserDataInsertDTO userDataInsert)
        {
            var userDataResponse = _mapper.Map<UserData.UserDataResponse>(userDataInsert);
            var userDataResult = await _userDataRepository.SetUserData(userDataResponse);
            return _mapper.Map<IEnumerable<UserDataDTO>>(userDataResult);
        }

        public async Task<IEnumerable<AddressDTO>> SetAdressData(AddressDataInsertDTO adressDataInsert)
        {
            var adress = _mapper.Map<UserData.Address>(adressDataInsert);
            var adressResult = await _userDataRepository.AddAdressData(adress);
            return _mapper.Map<IEnumerable<AddressDTO>>(adressResult);
        }
    }
}
