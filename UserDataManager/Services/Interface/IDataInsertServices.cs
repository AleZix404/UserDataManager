using Microsoft.AspNetCore.Mvc;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Services.Interface
{
    public interface IDataInsertServices<TGralDTO, TInserDTO, VGralDTO, VInsertDTO>
    {
        public Task<IEnumerable<TGralDTO>> AsignDataClient(IEnumerable<TInserDTO> userDataResponse);
        public Task<IEnumerable<TGralDTO>> AddUserData(TInserDTO dataInsertDTO);
        public Task<IEnumerable<VGralDTO>> SetAdressData(VInsertDTO otherDataInsertDTO);
    }
}
