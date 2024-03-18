
using Microsoft.EntityFrameworkCore;
using UserDataManager.EntityFramework.Models;

namespace UserDataManager.EntityFramework.Context
{
    public class UserDataContext : DbContext
    {
        public UserDataContext(DbContextOptions <UserDataContext> options) : base(options)
        {
        
        }

        public DbSet<UserData.UserDataResponse> UserDataResponse { get; set; }
        public DbSet<UserData.Address> Address { get; set; }
    }
}
