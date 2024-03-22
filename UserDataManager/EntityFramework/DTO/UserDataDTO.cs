using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserDataManager.EntityFramework.Models
{
    public class UserDataDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int IdAdress { get; set; }
        public UserData.Address? Address { get; set; }
    }
}
