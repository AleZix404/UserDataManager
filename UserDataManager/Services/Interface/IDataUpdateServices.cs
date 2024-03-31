using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Services.Interface
{
    public interface IDataUpdateServices<T, U>
    {
        public Task<U> UpdateData(T dataUpdate);
    }
}
