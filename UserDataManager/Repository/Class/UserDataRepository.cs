using Microsoft.EntityFrameworkCore;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Repository.Interface;

namespace UserDataManager.Repository.Class
{
    public class UserDataRepository : IRepository<UserData.UserDResp, UserData.Address>
    {
        private UserDataContext _userDataContext;

        public UserDataRepository(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
        }

        #region Create
        public async Task<IEnumerable<UserData.UserDResp>> SetDataList(IEnumerable<UserData.UserDResp> userDataResponse)
        {
            foreach (var userData in userDataResponse)
            {
                userData.Id = 0;
                await _userDataContext.UserDResp.AddAsync(userData);
                await _userDataContext.Address.AddAsync(userData.Address);
            }
            await _userDataContext.SaveChangesAsync();

            return userDataResponse;
        }
        public async Task<IEnumerable<UserData.UserDResp>> SetUserData(UserData.UserDResp userData)
        {
            await _userDataContext.UserDResp.AddAsync(userData);
            await _userDataContext.SaveChangesAsync();

            return await ReadAllUserData();
        }
        public async Task<IEnumerable<UserData.Address>> AddAdressData(UserData.Address adressData)
        {
            await _userDataContext.Address.AddAsync(adressData);
            await _userDataContext.SaveChangesAsync();

            return _userDataContext.Address;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<UserData.UserDResp>> ReadAllUserData() =>
        await _userDataContext.UserDResp.Include(u => u.Address).ToListAsync();

        public async Task<UserData.UserDResp> ReadUserData
        (int id) => await _userDataContext.UserDResp.AsNoTracking()
            .Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<UserData.Address> ReadAddressData
        (int id) => await _userDataContext.Address.FirstOrDefaultAsync(u => u.IdAdress == id);
        public UserData.UserDResp ReadAdressDataInUserData(int id) =>
        _userDataContext.UserDResp.FirstOrDefault(x => x.IdAdress == id);
        #endregion

        #region Update
        public async Task<UserData.UserDResp> UpdateUserData(UserData.UserDResp userData)
        {
            var userDataResult = await ReadUserData(userData.Id);
            
            if (userDataResult == null)
            {
                throw new Exception("user data not exist");
            }
            
            _userDataContext.UserDResp.Attach(userData);
            _userDataContext.Entry(userData).State = EntityState.Modified;
            await SaveChange();

            return await ReadUserData(userData.Id);
        }
        #endregion

        #region Delete
        public async Task RemoveData(int id)
        {
            var userDataResult = _userDataContext.UserDResp.FirstOrDefault(u => u.Id == id);
            _userDataContext.UserDResp.Remove(userDataResult);
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
            _userDataContext.UserDResp.RemoveRange(_userDataContext.UserDResp);
        }
        public void RemovedAllAddressData()
        {
            _userDataContext.Address.RemoveRange(_userDataContext.Address);
        }
        #endregion

        #region Utils
        public bool IsExistUserData()
        {
            return _userDataContext.UserDResp.Any();
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
