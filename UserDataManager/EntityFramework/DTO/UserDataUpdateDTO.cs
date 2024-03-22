using UserDataManager.EntityFramework.Models;

namespace UserDataManager.EntityFramework.DTO
{
    public class UserDataUpdateDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int IdAdress { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}
