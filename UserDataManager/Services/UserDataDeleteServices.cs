using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Repository.Class;
using UserDataManager.Repository.Interface;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataDeleteServices: IDataDeleteServices
    {
        private IRepository<UserData.UserDataResponse, UserData.Address> _userDataRepository;

        public UserDataDeleteServices(IRepository<UserData.UserDataResponse, UserData.Address> userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }

        public async Task<bool> RemoveData(int id)
        {
            var userData = await _userDataRepository.ReadUserData(id);

            if (userData != null)
            {
                await _userDataRepository.RemoveData(userData.Id);
            }
            return Convert.ToBoolean(userData);
        }
        public async Task<bool> RemoveOtherData(int id)
        {
            var userData = _userDataRepository.ReadUserDataInUserData(id);
            var adress = await _userDataRepository.ReadAddressData(id);
            bool isAdressRemoved = userData == null && adress != null;

            if (isAdressRemoved)
            {
                await _userDataRepository.RemoveOtherData(id);
            }
            return isAdressRemoved;
        }
        public async Task<bool> ClearAllUserDataClient()
        {
            bool isUserDataRemoved = _userDataRepository.IsExistUserData();
            bool isAdressDataRemoved = _userDataRepository.IsExistAddressData();
            bool isRemoveData = isUserDataRemoved || isAdressDataRemoved;

            if (isUserDataRemoved)
            {
                _userDataRepository.RemovedAllUserData();
            }
            if (isAdressDataRemoved)
            {
                _userDataRepository.RemovedAllAddressData();
            }
            if (isRemoveData)
            {
                await _userDataRepository.SaveChange();
            }
            return isRemoveData;
        }
    }
}
