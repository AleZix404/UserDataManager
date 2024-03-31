
using Microsoft.EntityFrameworkCore;
using UserDataManager.EntityFramework.Models;
using static UserDataManager.EntityFramework.Models.UserData;

namespace UserDataManager.EntityFramework.Context
{
    public class UserDataContext : DbContext
    {
        public UserDataContext(DbContextOptions <UserDataContext> options) : base(options)
        {
        
        }

        public DbSet<UserData.UserDResp> UserDResp { get; set; }
        public DbSet<UserData.Address> Address { get; set; }
    }
}
