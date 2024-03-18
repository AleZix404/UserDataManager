using Microsoft.EntityFrameworkCore;
using UserDataManager.EntityFramework.Context;
using UserDataManager.EntityFramework.Models;
using UserDataManager.Services.Interface;

namespace UserDataManager.Services
{
    public class UserDataReadServices: IReadDataServices<UserDataDTO>
    {
        private UserDataContext _userDataContext;

        public UserDataReadServices(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
        }

        public async Task<IEnumerable<UserDataDTO>> ReadAllUserDataList()
        {
            var userDataReadDTO = await _userDataContext.UserDataResponse.Select(userData => new UserDataDTO
            {
                Name = userData.Name,
                Email = userData.Email,
                Website = userData.Website
            }).ToListAsync();

            return userDataReadDTO;
        }
        public async Task<UserDataDTO> ReadUserData(int id)
        {
            var userData = await _userDataContext.UserDataResponse.FirstOrDefaultAsync(u => u.Id == id);

            var userDataReadDTO = new UserDataDTO
            {
                Id = id,
                Name = userData.Name,
                Email = userData.Email,
                Website = userData.Website
            };

            return userDataReadDTO;
        }
    }
}
