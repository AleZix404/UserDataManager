using Microsoft.EntityFrameworkCore;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataUpdateServices : IDataUpdateServices<UserDataDTO>
    {
        private UserDataContext _userDataContext;

        public UserDataUpdateServices(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
        }

        public async Task<UserDataDTO> UpdateUserData(UserDataDTO userDataReadDTO)
        {
            var userData = await _userDataContext.UserDataResponse.FirstOrDefaultAsync(u => u.Id == userDataReadDTO.Id);

            userData.Name = userDataReadDTO.Name;
            userData.Email = userDataReadDTO.Email;
            userData.Website = userDataReadDTO.Website;
            await _userDataContext.SaveChangesAsync();

            return userDataReadDTO;
        }
    }
}
