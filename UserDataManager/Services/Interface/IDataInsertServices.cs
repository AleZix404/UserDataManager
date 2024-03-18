using Microsoft.AspNetCore.Mvc;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Services.Interface
{
    public interface IDataInsertServices<T, U, V, K>
    {
        public Task<IEnumerable<T>> AsignDataClient(IEnumerable<T> dataResponse);
        public Task<IEnumerable<T>> AddUserData(U dataInsertDTO);
        public Task<IEnumerable<V>> AddAdressData(K otherDataInsertDTO);
    }
}
