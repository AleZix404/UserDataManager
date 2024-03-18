using UserDataManager.EntityFramework.Context;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataDeleteServices: IDataDeleteServices
    {
        private UserDataContext _userDataContext;

        public UserDataDeleteServices(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
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
