using Microsoft.EntityFrameworkCore;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Repository.Interface;

namespace UserDataManager.Repository.Class
{
    public class UserDataRepository : IRepository<UserData.UserDataResponse, UserData.Address>
    {
        private UserDataContext _userDataContext;

        public UserDataRepository(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
        }

        #region Create
        public async Task<IEnumerable<UserData.UserDataResponse>> SetDataList(IEnumerable<UserData.UserDataResponse> userDataResponse)
        {
            foreach (var userData in userDataResponse)
            {
                userData.Id = 0;
                await _userDataContext.UserDataResponse.AddAsync(userData);
                await _userDataContext.Address.AddAsync(userData.Address);
            }
            await _userDataContext.SaveChangesAsync();

            return userDataResponse;
        }
        public async Task<IEnumerable<UserData.UserDataResponse>> SetUserData(UserData.UserDataResponse userData)
        {
            await _userDataContext.UserDataResponse.AddAsync(userData);
            await _userDataContext.SaveChangesAsync();

            return _userDataContext.UserDataResponse;
        }
        public async Task<IEnumerable<UserData.Address>> AddAdressData(UserData.Address adressData)
        {
            await _userDataContext.Address.AddAsync(adressData);
            await _userDataContext.SaveChangesAsync();

            return _userDataContext.Address;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<UserData.UserDataResponse>> ReadAllUserDataList() =>
        await _userDataContext.UserDataResponse.ToListAsync();

        public async Task<UserData.UserDataResponse> ReadUserData
        (int id) => await _userDataContext.UserDataResponse.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<UserData.Address> ReadAddressData
        (int id) => await _userDataContext.Address.FirstOrDefaultAsync(u => u.IdAdress == id);
        public UserData.UserDataResponse ReadUserDataInUserData(int id) =>
        _userDataContext.UserDataResponse.FirstOrDefault(x => x.IdAdress == id);
        #endregion

        #region Update
        public async Task<UserData.UserDataResponse> UpdateUserData(UserData.UserDataResponse userData)
        {
            var userDataResult = await ReadUserData(userData.Id);

            _userDataContext.UserDataResponse.Attach(userDataResult);
            _userDataContext.UserDataResponse.Entry(userDataResult).State = EntityState.Modified;
            await SaveChange();

            return userDataResult;
        }
        #endregion

        #region Delete
        public async Task RemoveData(int id)
        {
            var userDataResult = _userDataContext.UserDataResponse.FirstOrDefault(u => u.Id == id);
            _userDataContext.UserDataResponse.Remove(userDataResult);
            await SaveChange();
        }
        public async Task RemoveOtherData(int id)
        {
            var addressDataResult = _userDataContext.Address.FirstOrDefault(u => u.IdAdress == id);
            _userDataContext.Address.Remove(addressDataResult);
            await SaveChange();
        }
        public void RemovedAllUserData()
        {
            _userDataContext.UserDataResponse.RemoveRange(_userDataContext.UserDataResponse);
        }
        public void RemovedAllAddressData()
        {
            _userDataContext.Address.RemoveRange(_userDataContext.Address);
        }
        #endregion

        #region Utils
        public bool IsExistUserData()
        {
            return _userDataContext.UserDataResponse.Any();
        }
        public bool IsExistAddressData()
        {
            return _userDataContext.Address.Any();
        }
        public async Task SaveChange()
        {
            await _userDataContext.SaveChangesAsync();
        }
        #endregion

    }
}
