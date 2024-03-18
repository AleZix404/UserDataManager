using Microsoft.AspNetCore.Mvc;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Services.Interface
{
    public interface IDataInsertServices<TEntity, UDTO, VEntity, KDTO>
    {
        public Task<IEnumerable<UDTO>> AsignDataClient(IEnumerable<TEntity> userDataResponse);
        public Task<IEnumerable<UDTO>> AddUserData(TEntity dataInsertDTO);
        public Task<IEnumerable<KDTO>> SetAdressData(VEntity otherDataInsertDTO);
    }
}
