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
    }
}
