using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Services.Interface
{
    public interface IUserDataClientServices
    {
        public Task<IEnumerable<UserData.UserDataResponse>> SetUserDataClient();
    }
}
