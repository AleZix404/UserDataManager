using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Repository.Interface
{
    public interface IRepository<TEntity, UEntity>
    {
        public Task<IEnumerable<TEntity>> SetDataList(IEnumerable<TEntity> userDataResponse);
        public Task<IEnumerable<TEntity>> SetUserData(TEntity userData);
        public Task<IEnumerable<UEntity>> AddAdressData(UEntity adressData);

        public Task<IEnumerable<TEntity>> ReadAllUserDataList();
        public Task<TEntity> ReadUserData(int id);
        public Task<UEntity> ReadAddressData(int id);
        public TEntity ReadUserDataInUserData(int id);

        public Task<TEntity> UpdateUserData(TEntity userDataReadDTO);

        public Task RemoveData(int id);
        public Task RemoveOtherData(int id);

        public bool IsExistUserData();
        public bool IsExistAddressData();
        public void RemovedAllUserData();
        public void RemovedAllAddressData();
        public Task SaveChange();
    }
}
