using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Repository.Interface
{
    public interface IRepository<TEntity, UEntity>
    {
        public Task<IEnumerable<TEntity>> SetDataList(IEnumerable<TEntity> userDataResponse);
        public Task<IEnumerable<TEntity>> SetUserData(TEntity userData);
        public Task<IEnumerable<UEntity>> AddAdressData(UEntity adressData);
    }
}
