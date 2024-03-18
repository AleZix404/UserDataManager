using Microsoft.AspNetCore.Mvc;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Services.Interface
{
    public interface IUserDataCRUDServices<T, U, V, K, M>
    {
        public Task<IEnumerable<T>> AsignDataClient(IEnumerable<T> DataResponse);
        public Task<IEnumerable<T>> AddUserData(V userDataInsertDTO);
        public Task<IEnumerable<K>> AddAdressData(M OtherDataInsertDTO);
        public Task<IEnumerable<U>> ReadAllUserDataList();
        public Task<U> ReadUserData(int id);
        public Task<U> UpdateUserData(U DataUpdateDTO);
        public Task<bool> RemoveData(int id);
        public Task<bool> RemoveOtherData(int id);
        public Task<bool> ClearAllUserDataClient();
    }
}
