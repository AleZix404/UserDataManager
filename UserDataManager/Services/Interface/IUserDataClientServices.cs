using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Services.Interface
{
    public interface IUserDataClientServices
    {
        public Task<IEnumerable<UserDataInsertDTO>> SetUserDataClient();
    }
}
